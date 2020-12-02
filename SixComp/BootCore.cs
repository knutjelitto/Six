using SixComp.Sema;
using SixComp.Support;
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

        public void Boot()
        {
            Console.WriteLine($"Boot {Sources.Name}");

            var compiler = new Compiler(Navi);

            var names = Sources.GetFiles("*.swift").Select(f => f.Name).ToList();

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

            var units = new List<Unit>();

            Console.Write("PARSE       ");
            foreach (var (name, context) in loaded)
            {
                Console.Write($".");

                var unit = compiler.Parse(context);

                if (unit == null)
                {
                    return;
                }

                units.Add(new Unit(context, unit));
            }
            Console.WriteLine();

            var package = new Package(units);

            var filename = Path.Combine(Temp.FullName, "_Analyze.txt");
            Directory.CreateDirectory(Path.GetDirectoryName(filename));
            using (var writer = new FileWriter(filename))
            {
                package.Analyze(writer);
            }


            Console.Write("DONE        ");
            foreach (var unit in package.Units)
            {
                Console.Write($".");
            }
            Console.WriteLine();
        }
    }
}
