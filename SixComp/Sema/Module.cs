using SixComp.Support;
using System;
using System.Collections.Generic;

namespace SixComp.Sema
{
    public class Module : IScoped
    {
        private readonly List<Unit> units = new List<Unit>();

        public Module(string moduleName)
        {
            Global = new Global();
            ModuleName = moduleName;
            Scope = new Scope(this, this);
        }

        public IReadOnlyList<Unit> Units => this.units;
        public IScope Scope { get; }
        public IScoped Outer => this;
        public Global Global { get; }
        public string ModuleName { get; }


        public static SortedSet<string> Missings = new SortedSet<string>();

        public void Add(Unit unit)
        {
            units.Add(unit);
        }

        public void Build(IWriter writer)
        {
            BuildTrees(writer);
            BuildOperators(writer);
            Report(writer);
        }

        public void Analyze(IWriter writer)
        {
            Resolve(writer);
        }

        private void Resolve(IWriter writer)
        {
            Console.Write("RESOLVE     ");

#if false
            Global.UnresolvedNames.Clear();
            using (writer.Indent("EXTENSIONS:"))
            {
                foreach (var extension in Global.Extensions)
                {
                    extension.ResolveExtended(writer);
                }
            }
            Global.UnresolvedNames.Report(writer, "UNRESOLVED:");
            writer.WriteLine();
#endif

            using (writer.Indent("UNITS:"))
            {
                foreach (var unit in Units)
                {
                    unit.Resolve(writer);
                    Console.Write($".");
                }
                Global.UnresolvedNames.Report(writer, "UNRESOLVED:");
            }
            writer.WriteLine();
            Console.WriteLine();
        }

        private void BuildTrees(IWriter writer)
        {
            Console.Write("BUILD       ");
            foreach (var unit in Units)
            {
                unit.Build();
                Console.Write($".");
            }
            Console.WriteLine();
        }

        private void BuildOperators(IWriter writer)
        {
            Console.Write("OPERATORS   ");
            Global.CreatePrecedences(this);
            Global.CreateOperators();
            Global.CreateInfixes();
            Console.WriteLine();
        }

        public void Report(IWriter writer)
        {
            Console.Write("REPORT      ");
            foreach (var unit in Units)
            {
                unit.Report(writer);
                Console.Write($".");
            }
            Console.WriteLine();

            ReportScoping(writer);
            writer.WriteLine();

            writer.WriteLine($"infixes-todo: #{Global.InfixesTodo.Count}");
            writer.WriteLine($"precedence-groups-todo: #{Global.PrecedencesTodo.Count}");
            writer.WriteLine($"operators-todo: #{Global.OperatorsTodo.Count}");
            writer.WriteLine();

            writer.WriteLine($"{Strings.Missing}s");
            using (writer.Indent())
            {
                var no = 1;
                foreach (var missing in Missings)
                {
                    writer.WriteLine($"{no,2}: {missing}");
                    no += 1;
                }
            }
            writer.WriteLine();
        }

        private void ReportScoping(IWriter writer)
        {
            using (writer.Indent($"scoping:"))
            {
                Scope.Report(writer);
            }
        }

        public override string ToString()
        {
            return $"{Strings.Head.Module} {ModuleName}";
        }
    }
}
