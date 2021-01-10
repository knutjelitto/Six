using Pegasus.Common;
using Six.Peg.Runtime;
using Six.Support;
using SixPeg.Expression;
using SixPeg.Matchers;
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

        public IEnumerable<TestFile> ManySwiftFiles(int no)
        {
            foreach (var file in Navi.SwiftCoreFull.EnumerateFiles("*.swift").Select(f => f.FullName.Replace('\\', '/')))
            {
                var skip = file.StartsWith("_");

                no += 1;
                yield return new TestFile(file, file, no, skip);
            }
        }

        public IEnumerable<TestFile> ManyGoFiles(int no)
        {
            var root = Path.Combine(Navi.Projects.FullName, "Languages", "go", "go", "src").Replace('\\', '/');

            var go = new DirectoryInfo(root);

            root += '/';

            foreach (var path in go.EnumerateFiles("*.go", SearchOption.AllDirectories).Skip(4166).Take(2).Select(f => f.FullName.Replace('\\', '/')))
            {
                var skip = path.EndsWith("/reflect/all_test.go") ||
                           path.EndsWith("/time/tzdata/zipdata.go") ||
                           path.EndsWith("/cmd/vendor/golang.org/x/sys/windows/zerrors_windows.go") ||
                           path.Contains("/bidi/tables") ||
                           path.Contains("/norm/tables") ||
                           path.Contains("/x86asm/tables") ||
                           path.Contains("/testdata/") ||
                           path.Contains("/x/net/idna/");

                no += 1;
                yield return new TestFile(path, path.Replace(root, ""), no, skip);
            }
        }

        public IEnumerable<TestFile> ManyTestFiles(string language, int no)
        {
            switch (language)
            {
                case "Swift":
                    foreach (var file in Navi.SwiftCoreFull.EnumerateFiles("*.swift").Select(f => f.FullName.Replace('\\', '/')))
                    {
                        var skip = file.StartsWith("_");

                        no += 1;
                        yield return new TestFile(file, file, no, skip);
                    }
                    break;
                case "Go":
                    {
                        var root = Path.Combine(Navi.Projects.FullName, "Languages", "go", "go", "src").Replace('\\', '/');

                        var go = new DirectoryInfo(root);

                        root += '/';

                        foreach (var path in go.EnumerateFiles("*.go", SearchOption.AllDirectories).Skip(4166).Take(2).Select(f => f.FullName.Replace('\\', '/')))
                        {
                            var skip = path.EndsWith("/reflect/all_test.go") ||
                                       path.EndsWith("/time/tzdata/zipdata.go") ||
                                       path.EndsWith("/cmd/vendor/golang.org/x/sys/windows/zerrors_windows.go") ||
                                       path.Contains("/bidi/tables") ||
                                       path.Contains("/norm/tables") ||
                                       path.Contains("/x86asm/tables") ||
                                       path.Contains("/testdata/") ||
                                       path.Contains("/x/net/idna/");

                            no += 1;
                            yield return new TestFile(path, path.Replace(root, ""), no, skip);
                        }
                    }
                    break;
                case "Pony":
                    {
                        var root = Path.Combine(Navi.Projects.FullName, "Languages", "Pony").Replace('\\', '/');

                        var pony = new DirectoryInfo(root);

                        root += '/';

                        foreach (var path in pony.EnumerateFiles("*.pony", SearchOption.AllDirectories).Select(f => f.FullName.Replace('\\', '/')).OrderBy(f => f))
                        {
                            var skip = false;
                            no += 1;
                            yield return new TestFile(path, path.Replace(root, ""), no, skip);
                        }
                    }
                    break;
                default:
                    yield break;
            }
        }


        public Parser CreateParser()
        {
            try
            {
                var grammar = CreateGrammar();
                return new Parser(GrammarName).Build(grammar);
            }
            catch (BailOutException)
            {
                return null;
            }
        }

        public Parser CreateParser(Grammar grammar)
        {
            return new Parser(GrammarName).Build(grammar);
        }

        public Grammar CreateGrammar()
        {
            var parser = new SixParser();

            var rules = new List<AnyRule>();
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
                    var cursor = ((Cursor)ex.Data["cursor"]).Location;

                    new Error(source).Report(ex.Message, cursor);

                    return null;
                }
            }

            var grammar = new Grammar(rules, options);

            return grammar;
        }

        public bool Test(Parser parser, List<TestFile> testFiles)
        {
            bool ok = false;
            var watch = new Stopwatch();

            foreach (var file in testFiles)
            {
                var skip = file.Skip ? "SKIP " : string.Empty;

                Console.WriteLine($"[{file.No}/{testFiles.Count}] {skip}{file.Name}");

                if (file.Skip)
                {
                    continue;
                }

                ok = false;

                var context = new Context(file.Path.FullName);

                file.Lines = context.Source.Index.Index.Count;

                if (!ok)
                {
                    try
                    {
                        var pegger = new PonyPeg(context);

                        watch.Reset();
                        watch.Start();
                        var match = pegger.Module(0);
                        file.Time = watch.Elapsed;

                        ok = match != null;
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
