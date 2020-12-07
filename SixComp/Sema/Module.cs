using SixComp.Support;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public void Analyze(IWriter writer)
        {
            BuildTrees(writer);
            Operators(writer);
            Report(writer);

            Resolve(writer);
            ReportUnresolved(writer);
        }

        private void Resolve(IWriter writer)
        {
            Console.Write("RESOLVE     ");
            foreach (var unit in Units)
            {
                unit.Resolve(writer);
                Console.Write($".");
            }
            writer.WriteLine();
            Console.WriteLine();
        }

        private void ReportUnresolved(IWriter writer)
        {
            using (writer.Indent("UNRESOLVED:"))
            {
                foreach (var unresolved in Global.UnresolvedNames.OrderBy(s => s))
                {
                    writer.WriteLine($"{unresolved}");
                }
            }
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

        private void Operators(IWriter writer)
        {
            Console.Write("OPERATORS   ");
            Global.CreatePrecedences(this);
            Global.CreateOperators();
            Global.CreateInfixes();
            Console.WriteLine();
        }

        private void Report(IWriter writer)
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
