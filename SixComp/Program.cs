using SixComp.Support;
using System;
using System.IO;
using System.Linq;

namespace SixComp
{
    internal class Program
    {
        //private bool TRUE = true;
        private DirectoryInfo WorkSpace;
        private DirectoryInfo TempDir;
        private DirectoryInfo SwiftDir;

        static void Main(string[] args)
        {
            new Program().Checker();
            //new Program().Swift();
            //SixRT.PlayCheck();
            Console.Write("(almost) any key ... ");
            Console.ReadKey(true);
        }

        public Program()
        {
            WorkSpace = new DirectoryInfo("../../../..");
            TempDir = new DirectoryInfo(Path.Combine(WorkSpace.FullName, "../Temp/Six"));
            TempDir.Create();
            SwiftDir = new DirectoryInfo(Path.Combine(WorkSpace.FullName, "../Swift"));

            //Console.WriteLine($"workspace: {WorkSpace.FullName}");
            //Console.WriteLine($"temp     : {TempDir.FullName}");
        }

        private void Error(Source source, string error, int start, int length)
        {
            Console.WriteLine($"error: {error}");
            var info = source.Index.GetInfo(start);
            Console.WriteLine($"    --> {source.Name}[{info.lineNumber},{info.columnNumber}]");
            if (info.lineNumber == 1)
            {
                Console.WriteLine($"     |");
            }
            else
            {
                for (var i = info.lineNumber - 1 - 15; i < info.lineNumber - 1; i += 1)
                {
                    var line = source.Index.GetLine(i);
                    if (line != null)
                    {
                        Console.WriteLine($"     | {line}");
                    }
                }
            }
            Console.WriteLine($"{info.lineNumber,4} | {info.line}");
            var arrow = length > 1 ? $"^{new string('-', length - 2)}^" : "^";
            Console.WriteLine($"     = {new string(' ', info.columnNumber - 1)}{arrow}");
            Console.WriteLine($"     = {new string(' ', info.columnNumber - 1)}`-- {error}");
            Console.WriteLine($"     =");
            for (var i = info.lineNumber; i < info.lineNumber + 3; i += 1)
            {
                var line = source.Index.GetLine(i);
                if (line != null)
                {
                    Console.WriteLine($"     | {line}");
                }
            }
        }

        private void Swift()
        {
            EnumSwifts("swift-numerics");
            //EnumSwifts("swift");
            //EnumSwifts("swift-package-manager\\Sources");
        }

        private void EnumSwifts(string swift)
        {
            var root = SwiftDir.FullName;
            var files = Directory.EnumerateFiles(Path.Combine(root, swift), "*.swift", SearchOption.AllDirectories)
                .Where(n => !n.EndsWith("Template.swift"))
                .ToList();
            Console.WriteLine($"hunting in {swift} ({files.Count} to go)");

            const int skip = 0;
            const int take = 100000;
            foreach (var file in files.Skip(skip).Take(take))
            {
                var name = file.Substring(root.Length + 1);
                var text = File.ReadAllText(file);
                Console.WriteLine($"FILE: {name}");
                if (!Test(new Source(name, text)))
                {
                    break;
                }
            }
        }

        public void Checker()
        {
#if false
            var file = @"./Source/Package.swift";
#endif
            var file = @"./Source/Checker.swift";
            var text = File.ReadAllText(file);
            Console.WriteLine($"FILE: {file}");
            Test(new Source(file, text));
        }

        public bool Test(Source source)
        {
            try
            {
                var index = new SourceIndex(source);
                var lexer = new Lexer(source);
                while (!lexer.Done)
                {
                    var token = lexer.GetNext();
                    var line = index.GetInfo(token.Span.Start);

                    if (token.Kind == ToKind.ERROR)
                    {
                        Error(source, "can't continue lexing (illegal character in input stream)", lexer.Start, 1);

                        break;
                    }
                }

                if (!lexer.Done)
                {
                    return false;
                }

                lexer = new Lexer(source);
                var parser = new Parser(lexer);

                var tree = parser.Parse();

                if (parser.Current != ToKind.EOF)
                {
                    var span = parser.CurrentToken.Span;

                    Error(source, $"can't continue parsing at `{parser.Current.GetRep()}`", span.Start, span.Length);

                    return false;
                }

                var treeFile = Path.Combine(TempDir.FullName, Path.ChangeExtension(source.Name, ".tree"));
                Directory.CreateDirectory(Path.GetDirectoryName(treeFile));
                using (var writer = new FileWriter(treeFile))
                {
                    tree.Write(writer);
                }
            }
            catch (ParserException pe)
            {
                Error(source, pe.Message, pe.Token.Span.Start, pe.Token.Span.Length);

                return false;

            }
            catch (LexerException le)
            {
                Error(source, le.Message, le.Offset, 1);

                return false;
            }

            return true;
        }
    }
}
