using Pegasus.Common;
using Six.Support;
using SixPeg.Expression;
using SixPeg.Matchers;
using SixPeg.Matches;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            return Navi.SwiftCoreFull.EnumerateFiles("*.swift").Where(f => !f.Name.StartsWith('_')).Select(f => f.FullName).ToList();
        }

        public IEnumerable<string> ManyGoFiles()
        {
            var go = new DirectoryInfo(Path.Combine(Navi.Projects.FullName, "Languages", "go", "go", "src"));

            return go.EnumerateFiles("*.go", SearchOption.AllDirectories).Select(f => f.FullName);
        }

        public Grammar CreateGrammar()
        {
            var parser = new SixParser();

            var rules = new List<AnyRule>();

            foreach (var grammarFile in GrammarFiles())
            {
                var source = new Source(grammarFile);

                try
                {
                    var part = parser.Parse(source);
                    rules.AddRange(part.Grules.OfType<Rules>().SelectMany(rs => rs.Cast<RuleExpression>()));
                    rules.AddRange(part.Grules.OfType<Terminals>().SelectMany(rs => rs.Cast<TerminalExpression>()));
                }
                catch (FormatException ex)
                {
                    var cursor = ((Cursor)ex.Data["cursor"]).Location;

                    new Error(source).Report(ex.Message, cursor);

                    return null;
                }
            }

            var grammar = new Grammar(rules.ToList());

            var tempFolder = Navi.TempFor(Navi.Project).FullName;

            using var writer = new FileWriter($"{tempFolder}/Matchers.txt");

            new ResolveVisitor(grammar, writer).Resolve();

            grammar.ResolveReferences();
            grammar.ReportMatchers(writer);

            return grammar;
        }

        public bool Test(Grammar grammar, IEnumerable<string> testFiles)
        {
            bool ok = false;

            foreach (var test in testFiles)
            {
                Console.WriteLine($"{test}");

                ok = false;

                var subject = new Context(test);
                int cursor;

                try
                {
                    if (ok)
                    {
                        cursor = 0;
                        grammar.Clear();
                        ok = grammar.GetMatcher().Match(subject, ref cursor);

                        if (!ok)
                        {
                            new Error(subject).Report("parse failed", cursor);
                        }
                    }
                }
                catch
                {
                    ok = false;
                }

                if (!ok)
                {
                    try
                    {
                        cursor = 0;
                        grammar.Clear();
                        var trees = grammar.GetMatcher().Matches(subject, cursor).ToList();
                        ok = trees.Count == 1;

                        if (!ok)
                        {
                            if (trees.Count > 1)
                            {
                                bool differ = IMatch.Differ(trees[0], trees[1]);

                                new Error(subject).Report($"far too many trees (#{trees.Count}) (differ:{differ})", subject.Length);
                            }
                            else
                            {
                                new Error(subject).Report("parse failed", AnyMatcher.furthestCursor);
                            }
                        }
                        else
                        {
                            Debug.Assert(true);
                        }
                    }
                    catch
                    {
                        ok = false;
                    }

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
