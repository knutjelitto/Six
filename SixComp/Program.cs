using SixComp.Support;
using System;
using System.IO;
using System.Linq;

#pragma warning disable IDE0051 // Remove unused private members

namespace SixComp
{
    internal class Program
    {
        //private bool TRUE = true;
        private DirectoryInfo WorkSpace;
        private DirectoryInfo TempDir;
        private DirectoryInfo SwiftDir;

        const int SkipFiles = 0;
        const int TakeFiles = 100000;

        static void Main(string[] args)
        {
            if (new Program().Checker())
            {
                new Program().Swift();
            }


            //SixRT.PlayCheck();
            Console.Write("(almost) any key ... ");
            Console.ReadKey(true);
        }

        private void Swift()
        {
            //EnumSwifts("swift-numerics");
            //EnumSwifts("swift/stdlib/public/core");
            EnumSwifts("swift/stdlib/public");
            //EnumSwifts("swift-package-manager/Sources");
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

        private void EnumSwifts(string swift)
        {
            var root = SwiftDir.FullName;
            var swiftRoot = Path.Combine(root, swift);
            var files = Directory.EnumerateFiles(swiftRoot, "*.swift", SearchOption.AllDirectories)
                .Where(n => !n.EndsWith("Template.swift"))
                .OrderBy(n => n.Substring(swiftRoot.Length))
                .ToList();
            Console.WriteLine($"hunting in {swift} ({files.Count} to go)");

            var count = files.Count;
            var digits = (int)Math.Truncate(Math.Log10(count)) + 1;

            files = files.Skip(SkipFiles).Take(TakeFiles).ToList();
            for (var i = 0; i < files.Count; i += 1)
            {
                var file = files[i];
                var name = file.Substring(root.Length + 1);
                var text = File.ReadAllText(file);
                var index = i + 1 + SkipFiles;
                var fill = new string(' ', digits - ((int)Math.Truncate(Math.Log10(index)) + 1));
                var currentDigits = (int)Math.Truncate(Math.Log10(index)) + 1;
                Console.WriteLine($"FILE({fill}{index}/{count}: {name}");
                if (!Test(new Context(name, text)))
                {
                    break;
                }
            }
        }

        public bool Checker()
        {
#if false
            var file = @"./Source/Package.swift";
#endif
            var file = @"./Source/Checker.swift";
            var text = File.ReadAllText(file);
            Console.WriteLine($"FILE: {file}");
            return Test(new Context(file, text));
        }

        public bool Test(Context context)
        {
            var parser = context.Parser;
            var source = context.Source;

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
            catch (InvalidOperationException invalid)
            {
                Console.WriteLine(invalid.ToString());
                Error(source, $"internal error - {invalid.Message}", parser.CurrentToken.Span.Start, parser.CurrentToken.Span.Length);

                return false;
            }
            catch (Exception any)
            {
                Console.WriteLine(any.ToString());

                return false;
            }

            return true;
        }
    }
}
