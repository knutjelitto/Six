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
        public BootCore(Navi navi)
        {
            Navi = navi;

            Temp.Create();
        }

        public Navi Navi { get; }
        public DirectoryInfo Core => Navi.SixCore;
        public DirectoryInfo Temp => Navi.TempFor(Navi.SixCore);

        public void Boot()
        {
            Console.WriteLine("Boot Core");

            var compiler = new Compiler(Navi);

            var names = new List<string>
            {
                "Policy.swift",
            };

            names = Navi.SixCore.GetFiles("*.swift").Select(f => f.Name).ToList();

            var loaded = new List<(string name, Context context)>();

            Console.Write("LOAD        ");
            foreach (var name in names)
            {
                Console.Write(".");
                var file = Navi.FileIn(Navi.SixCore, name);
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
