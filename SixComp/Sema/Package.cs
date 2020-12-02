using SixComp.Sema.Decls;
using SixComp.Support;
using System;
using System.Collections.Generic;

namespace SixComp.Sema
{
    public class Package : IScoped
    {
        public Package(IReadOnlyList<Unit> units)
        {
            Units = units;

            PrecedencesTodo = new Queue<PrecedenceGroup>();
            OperatorsTodo = new Queue<OperatorDecl>();
            InfixesTodo = new Queue<ExpressionList>();

            Precedences = new Dictionary<BaseName, PrecedenceGroup>();
        }

        public IReadOnlyList<Unit> Units { get; }
        public IScope Scope => new Scope(this, this);

        public Dictionary<BaseName, PrecedenceGroup> Precedences { get; }

        public Queue<PrecedenceGroup> PrecedencesTodo { get; }
        public Queue<OperatorDecl> OperatorsTodo { get; }
        public Queue<ExpressionList> InfixesTodo { get; }

        public static SortedSet<string> MissingTreeImplementations = new SortedSet<string>();

        public void Analyze(IWriter writer)
        {
            Console.Write("BUILD-TREE  ");
            foreach (var unit in Units)
            {
                Console.Write($".");
                unit.BuildTree(this);
            }
            Console.WriteLine();

            Console.Write("REPORT      ");
            foreach (var unit in Units)
            {
                Console.Write($".");
                writer.WriteLine($"{unit.Short}:");
                using (writer.Indent())
                {
                    unit.Report(writer);
                }
            }
            Console.WriteLine();

            writer.WriteLine($"infixes-todo: #{InfixesTodo.Count}");
            writer.WriteLine($"precedence-groups-todo: #{PrecedencesTodo.Count}");
            writer.WriteLine($"operators-todo: #{OperatorsTodo.Count}");
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
