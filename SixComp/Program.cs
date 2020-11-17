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
            //new Program().Test();
            //SixRT.PlayCheck();
            new Program().Swift();
            Console.Write("(almost) any key ... ");
            Console.ReadKey(true);
        }

        public Program()
        {
            WorkSpace = new DirectoryInfo("../../../..");
            TempDir = new DirectoryInfo(Path.Combine(WorkSpace.FullName, "../Temp/Six"));
            TempDir.Create();
            SwiftDir = new DirectoryInfo(Path.Combine(WorkSpace.FullName, "../Swift"));

            Console.WriteLine($"workspace: {WorkSpace.FullName}");
            Console.WriteLine($"temp     : {TempDir.FullName}");
        }

        private void Swift()
        {
            EnumSwifts("swift-package-manager\\Sources");
        }

        private void EnumSwifts(string swift)
        {
            var root = SwiftDir.FullName;
            var files = Directory.EnumerateFiles(Path.Combine(root, swift), "*.swift", SearchOption.AllDirectories);

            foreach (var file in files.Take(10))
            {
                var name = file.Substring(root.Length + 1);
                var text = File.ReadAllText(file);
                if (!Test(new Source(name, text)))
                {
                    break;
                }
            }
        }

        public void Test()
        {
#if false
            var file = @"./Source/Package.swift";
#endif
            var file = @"./Source/Six.swift";
            var text = File.ReadAllText(file);
            Test(new Source(file, text));
        }

        public bool Test(Source source)
        {
            var index = new SourceIndex(source);
            var lexer = new Lexer(source);
            while (!lexer.Done)
            {
                var token = lexer.GetNext();
                var line = index.GetLine(token.Span.Start);
                Console.WriteLine($"@({line.lineNumber},{line.columnNumber}) {token.Kind} [{token.Span.Start}..<{token.Span.End}]`{token.Span}` on line `{line.line}`");
                if (token.Kind == ToKind.ERROR)
                {
                    break;
                }
            }

            if (!lexer.Done)
            {
                Console.WriteLine($"lexer error at >>>{lexer.Rest}");
                return false;
            }

            lexer = new Lexer(source);
            var parser = new Parser(lexer);

            try
            {
                Console.WriteLine($"parsing \"\"\"");
                Console.Write(source.Content);
                Console.WriteLine("\"\"\"");

                var tree = parser.Parse();

                if (parser.Current.Kind != ToKind.EOF)
                {
                    Console.WriteLine($"error: can't continue parsing at `{parser.Current}`");
                    var lineinfo = source.Index.GetLine(parser.Current.Span.Start);
                    Console.WriteLine($"    --> {source.Name}[{lineinfo.lineNumber},{lineinfo.columnNumber}]");
                    Console.WriteLine($"     |");
                    Console.WriteLine($"{lineinfo.lineNumber,4} | {lineinfo.line}");
                    var arrow = parser.Current.Span.Length > 1 ? $"^{new string('-', parser.Current.Span.Length - 2)}^" : "^";
                    Console.WriteLine($"     | {new string(' ', lineinfo.columnNumber-1)}{arrow}");

                    return false;
                }

                var treeFile = Path.Combine(TempDir.FullName, Path.ChangeExtension(source.Name, ".tree"));
                Directory.CreateDirectory(Path.GetDirectoryName(treeFile));
                using (var writer = new FileWriter(treeFile))
                {
                    tree.Write(writer);
                }
            }
            catch (InvalidOperationException error)
            {
                Console.WriteLine($"error: {error.Message}");
                Console.WriteLine($"error: can't continue parsing at `{parser.Current}`");
                var lineinfo = source.Index.GetLine(parser.Current.Span.Start);
                Console.WriteLine($"    --> {source.Name}[{lineinfo.lineNumber},{lineinfo.columnNumber}]");
                Console.WriteLine($"     |");
                Console.WriteLine($"{lineinfo.lineNumber,4} | {lineinfo.line}");
                var arrow = parser.Current.Span.Length > 1 ? $"^{new string('-', parser.Current.Span.Length - 2)}^" : "^";
                Console.WriteLine($"     | {new string(' ', lineinfo.columnNumber - 1)}{arrow}");

                return false;
            }

            return true;
        }
    }
}
