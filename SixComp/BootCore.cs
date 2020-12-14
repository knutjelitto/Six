using Six.Support;
using SixComp.Sema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SixComp
{
    public class BootCore
    {
        public BootCore(Navi navi, DirectoryInfo sources)
        {
            Navi = navi;
            Sources = sources;
            Temp = Navi.TempFor(Sources);
            Temp.Create();
        }

        public Navi Navi { get; }
        public DirectoryInfo Sources { get; }
        public DirectoryInfo Temp { get; }

        public void Compile(string moduleName)
        {
            Console.WriteLine($"Boot {Sources.Name}");

            var compiler = new Compiler(Navi);

#if false
            var peg = new Peg.Parser();
            //peg.Tracer = Pegasus.Common.Tracing.DiagnosticsTracer.Instance;
#endif

            var names = Sources.GetFiles("*.swift").Select(f => f.Name).Where(n => !n.StartsWith('_')).ToList();

            var loaded = new List<(string name, Context context)>();

            Console.Write("LOAD        ");
            foreach (var name in names)
            {
                Console.Write(".");
                var file = Navi.FileIn(Sources, name);
                var context = new Context(file, Temp, File.ReadAllText(file.FullName));
                loaded.Add((name, context));
            }
            Console.WriteLine();

            var module = new Module(moduleName);

            Console.Write("PARSE       ");
            foreach (var (_, context) in loaded)
            {
                Console.Write($".");

#if false
                try
                {
                    peg.Parse(context.Source.Content);
                }
                catch (FormatException error)
                {
                    Console.WriteLine();
                    Console.WriteLine($"ERROR: {error}");
                }
#endif

                var unit = compiler.Parse(new ConsoleWriter(), context);

                if (unit == null)
                {
                    return;
                }

                module.Add(new Unit(context, module, unit));
            }
            Console.WriteLine();

            using (var writer = new FileWriter(EnsureTemp("_Build.txt")))
            {
                module.Build(writer);
            }

            using (var writer = new FileWriter(EnsureTemp("_Analyze.txt")))
            {
                module.Analyze(writer);
            }


            Console.Write("DONE        ");
            foreach (var unit in module.Units)
            {
                Console.Write($".");
            }
            Console.WriteLine();
        }

        private string EnsureTemp(string file)
        {
            var filename = Path.Combine(Temp.FullName, file);
            Directory.CreateDirectory(Path.GetDirectoryName(filename)!);
            return filename;
        }
    }
}
