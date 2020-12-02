using SixComp.Support;
using System.Collections.Generic;

namespace SixComp.Sema
{
    public class PrecedenceGroup : Base<Tree.PrecGroupDeclaration>, IDeclaration
    {
        public enum Associativity
        {
            None,
            Left,
            Right,
        }

        public PrecedenceGroup(IScoped outer, Tree.PrecGroupDeclaration tree)
            : base(outer, tree)
        {
            Name = new BaseName(outer, tree.Name);
            Assoc = Associativity.None;
            Assign = false;
            HigherThan = new Dictionary<BaseName, PrecedenceGroup>();
            LowerThan = new Dictionary<BaseName, PrecedenceGroup>();
        }

        public BaseName Name { get; }
        public Associativity Assoc { get; private set; }
        public bool Assign { get; private set; }
        public Dictionary<BaseName, PrecedenceGroup> HigherThan { get; }
        public Dictionary<BaseName, PrecedenceGroup> LowerThan { get; }

        public override void Report(IWriter writer)
        {
            writer.WriteLine(Strings.Precedence);
            Name.Report(writer, Strings.Head.Name);
            Assoc.Report(writer, Strings.Head.Assoc);
            Assign.Report(writer, Strings.Head.Assign);
        }
    }
}
