using System;
using System.IO;
using System.Linq;

#pragma warning disable IDE0051 // Remove unused private members

namespace SixComp
{
    internal class Program
    {
        //private bool TRUE = true;

        private readonly Navi Navi;

        const int SkipFiles = 0;
        const int TakeFiles = 100000;

        static void Main(string[] args)
        {
            new BootCore(new Navi()).Boot();

            //if (new Program().Checker()) { new Program().Swift(); }


            //SixRT.PlayCheck();
            Console.Write("(almost) any key ... ");
            Console.ReadKey(true);
        }

        private void Swift()
        {
            //EnumSwifts("swift-numerics");
            //EnumSwifts("swift/stdlib/public/core");
            //EnumSwifts("swift/stdlib/public");
            //EnumSwifts("swift/stdlib");
            //EnumSwifts("swift-package-manager/Sources");
            //EnumSwifts("others/RxSwift");
            EnumSwifts("others/swift-algorithm-club");
        }

        private bool FilterOut(string name)
        {
            return name.EndsWith("Template.swift")
                || name.Contains("/private/StdlibUnittestFoundationExtras/")
                || name.Contains("/public/Concurrency/")
                || name.Contains("/public/Darwin/")
                || name.Contains("/VisualizedDijkstra.playground/")
                || name.EndsWith("StdlibUnittest.swift")
                || name.EndsWith("/private/SwiftPrivate/SwiftPrivate.swift")
                ;
        }

        public Program()
        {
            Navi = new Navi();
        }

        private void EnumSwifts(string swift)
        {
            var compiler = new Compiler(Navi);

            var root = Navi.SwiftSources.FullName;
            var swiftRoot = Path.Combine(root, swift);
            var files = Directory.EnumerateFiles(swiftRoot, "*.swift", SearchOption.AllDirectories)
                .OrderBy(n => n.Substring(swiftRoot.Length))
                .Select(n => n.Replace('\\', '/'))
                .Where(n => !FilterOut(n))
                .ToList();
            Console.WriteLine($"hunting in {swift} ({files.Count} to go)");

            var count = files.Count;
            var digits = (int)Math.Truncate(Math.Log10(count)) + 1;

            files = files.Skip(SkipFiles).Take(TakeFiles).ToList();
            for (var i = 0; i < files.Count; i += 1)
            {
                var filepath = files[i];
                var file = new FileInfo(filepath);
                var name = filepath.Substring(root.Length + 1);
                var temp = Navi.TempFor(Path.GetDirectoryName(name) ?? string.Empty);
                var text = File.ReadAllText(filepath);
                var index = i + 1 + SkipFiles;
                var fill = new string(' ', digits - ((int)Math.Truncate(Math.Log10(index)) + 1));
                var currentDigits = (int)Math.Truncate(Math.Log10(index)) + 1;
                Console.WriteLine($"FILE({fill}{index}/{count}: {name}");
                var context = new Context(file, temp, text);
                var unit = compiler.Parse(context);
                if (unit == null)
                {
                    break;
                }
                Dumper.ParsedSwift(context, unit);
            }
        }

        public bool Checker()
        {
#if true
            return true;
#else
            var file = @"./Source/Checker.swift";
            var text = File.ReadAllText(file);
            Console.WriteLine($"FILE: {file}");
            return new Compiler(Navi).Parse(new Context(file, text));
#endif
        }
    }
}
