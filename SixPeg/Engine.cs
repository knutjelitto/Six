using Pegasus.Common;
using Six.Support;
using SixPeg.Expression;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SixPeg
{
    public class Engine
    {
        public Navi Navi { get; }
        public string GrammarName { get; }

        public Engine(string grammarName)
        {
            Navi = new Navi();
            GrammarName = grammarName;
        }

        public List<string> GrammarFiles()
        {
            var files = new List<string>();

            var grammarFolder = new DirectoryInfo(Path.Combine(Navi.Project.FullName, "Grammars", GrammarName));

            var stem = new FileInfo(Path.Combine(grammarFolder.FullName, $"{GrammarName}.sixpeg"));

            files.Add(stem.FullName);

            foreach (var file in grammarFolder.GetFiles("*.sixpeg"))
            {
                if (file.Name != stem.Name)
                {
                    files.Add(file.FullName);
                }
            }

            return files;
        }

        public List<string> AllTestFiles()
        {
            return Navi.SixCoreFull.EnumerateFiles("*.swift").Where(f => !f.Name.StartsWith('_')).Select(f => f.FullName).ToList();
        }

        public GrammarExpression CreateGrammar()
        {
            var parser = new SixParser();

            var rules = new List<RuleExpression>();

            foreach (var grammarFile in GrammarFiles())
            {
                var source = new Source(grammarFile);

                try
                {
                    rules.AddRange(parser.Parse(source));

                }
                catch (FormatException ex)
                {
                    var cursor = ((Cursor)ex.Data["cursor"]).Location;

                    new Error(source).Report(ex.Message, cursor);

                    return null;
                }
            }

            var grammar = new GrammarExpression(rules.ToList());

            using var writer = new FileWriter("../../../Reports/Matchers.txt");

            new ResolveVisitor(grammar, writer).Resolve();

            grammar.ReportMatchers(writer);

            return grammar;
        }

        public bool Test(GrammarExpression grammar, IEnumerable<string> testFiles)
        {
            bool ok = false;

            foreach (var test in testFiles)
            {
                var subject = new Context(test);
                var cursor = 0;

                try
                {
                    grammar.Clear();
                    ok = grammar.GetMatcher().Match(subject, ref cursor);
                }
                catch
                {
                    ok = false;
                }

                if (!ok)
                {
                    break;
                }
            }

            return ok;
        }
    }
}
