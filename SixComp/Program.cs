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

            const int skip = 4;
            const int take = 10000;
            foreach (var file in files.Skip(skip).Take(take))
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
            Console.WriteLine($"FILE: {source.Name}");
            //Console.WriteLine($"parsing \"\"\"");
            //Console.Write(source.Content);
            //Console.WriteLine("\"\"\"");

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

            try
            {
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
            catch (InvalidOperationException error)
            {
                var span = parser.CurrentToken.Span;
                Error(source, error.Message, span.Start, span.Length);

                return false;
            }

            return true;
        }
    }
}
