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
        public Global Global { get; }
        public string ModuleName { get; }

        public static SortedSet<string> Missings = new SortedSet<string>();

        public void Add(Unit unit)
        {
            units.Add(unit);
        }

        public void Analyze(IWriter writer)
        {
            Console.Write("BUILD-TREE  ");
            foreach (var unit in Units)
            {
                unit.BuildTree(this);
                Console.Write($".");
            }
            Console.WriteLine();


            Global.CreatePrecedences(this);
            Global.CreateOperators();
            Global.CreateInfixes();

            Console.Write("REPORT      ");
            foreach (var unit in Units)
            {
                unit.Report(writer);
                Console.Write($".");
            }
            Console.WriteLine();

            Scope.Report(writer);
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
        }

        public override string ToString()
        {
            return $"{Strings.Head.Module} {ModuleName}";
        }

        public void AddUnique(INamed named)
        {
            throw new NotImplementedException();
        }
    }
}
