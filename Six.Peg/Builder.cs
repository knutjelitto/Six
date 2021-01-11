using Six.Peg.Runtime;
using Six.Support;
using Six.Peg.Expression;
using SixPeg.Matchers;
using SixPeg.Visiting;
using SixPeg.Writing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SixPeg
{
    public class Builder
    {
        private readonly Navi navi;
        private readonly DirectoryInfo destination;
        private readonly DirectoryInfo grammarDir;
        private readonly DirectoryInfo generatedDir;

        private readonly string grammarName = "Swift";

        public Builder()
        {
            navi = new Navi();

            destination = navi.GetProject("Six.Comp");

            grammarDir = navi.Subdir(destination, "Grammar");
            generatedDir = navi.Subdir(destination, "Generated");
        }

        public void Build()
        {
            var parser = CreateParser();

            using (var writer = new FileWriter(navi.File(generatedDir, "parser.txt").FullName))
            {
                new PrintVisitor(writer).Print(parser);
            }

            using (var writer = new FileWriter(navi.File(generatedDir, "Pegger.cs").FullName))
            {
                new Emitter(parser, writer).Emit();
            }
        }


        private Parser CreateParser()
        {
            try
            {
                var grammar = CreateGrammar();
                return new Parser(grammarName).Build(grammar);
            }
            catch (BailOutException)
            {
                return null;
            }
        }

        private Grammar CreateGrammar()
        {
            var parser = new SixParser();

            var rules = new List<Rule>();
            var options = new List<OptionExpression>();

            foreach (var grammarFile in GrammarFiles())
            {
                var source = new Source(grammarFile);

                try
                {
                    var part = parser.Parse(source);
                    options.AddRange(part.Grules.OfType<Options>().SelectMany(rs => rs.Cast<OptionExpression>()));
                    rules.AddRange(part.Grules.OfType<Rules>().SelectMany(rs => rs.Cast<RuleExpression>()));
                    rules.AddRange(part.Grules.OfType<Terminals>().SelectMany(rs => rs.Cast<TerminalExpression>()));
                }
                catch (FormatException ex)
                {
                    var cursor = ((Pegasus.Common.Cursor)ex.Data["cursor"]).Location;

                    new Error(source).Report(ex.Message, cursor);

                    return null;
                }
            }

            var grammar = new Grammar(rules, options);

            return grammar;
        }

        private List<string> GrammarFiles()
        {
            var files = new List<string>();

            var grammarFolder = grammarDir;

            var stem = new FileInfo(Path.Combine(grammarFolder.FullName, $"{grammarName}.sixpeg"));

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
    }
}
