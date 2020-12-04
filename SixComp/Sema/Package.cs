using SixComp.Support;
using System;
using System.Collections.Generic;

namespace SixComp.Sema
{
    public class Package : IScoped
    {
        private readonly List<Unit> units = new List<Unit>();

        public Package()
        {

            Global = new Global();
        }

        public IReadOnlyList<Unit> Units => this.units;
        public IScope Scope => new Scope(this, this);

        public Global Global { get; }

        public Dictionary<BaseName, PrecedenceGroupDeclaration> Precedences => Global.Precedences;

        public static SortedSet<string> MissingTreeImplementations = new SortedSet<string>();

        public void Add(Unit unit)
        {
            units.Add(unit);
        }

        public void Analyze(IWriter writer)
        {
            Console.Write("BUILD-TREE  ");
            foreach (var unit in Units)
            {
                Console.Write($".");
                unit.BuildTree(this);
            }
            Console.WriteLine();


            Global.CreatePrecedences(this);
            Global.CreateOperators();
            Global.CreateInfixes();

            Console.Write("REPORT      ");
            foreach (var unit in Units)
            {
                Console.Write($".");
                unit.Report(writer);
            }
            Console.WriteLine();

            writer.WriteLine($"infixes-todo: #{Global.InfixesTodo.Count}");
            writer.WriteLine($"precedence-groups-todo: #{Global.PrecedencesTodo.Count}");
            writer.WriteLine($"operators-todo: #{Global.OperatorsTodo.Count}");
            writer.WriteLine();

            writer.WriteLine($"{Strings.Missing}s");
            using (writer.Indent())
            {
                var no = 1;
                foreach (var missing in MissingTreeImplementations)
                {
                    writer.WriteLine($"{no,2}: {missing}");
                    no += 1;
                }
            }
        }

        public void AddUnique(INamed named)
        {
            throw new NotImplementedException();
        }
    }
}
