using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class PatternVar : Items<PatternInitializer, Tree.PatternVarDeclaration>, IDeclaration
    {
        public PatternVar(IScoped outer, Tree.PatternVarDeclaration tree)
            : base(outer, tree, Enum(outer, tree))
        {
        }

        public override void Report(IWriter writer)
        {
            writer.WriteLine($";; {Tree}");
            this.ReportList(writer, Strings.Head.Var);
        }

        private static IEnumerable<PatternInitializer> Enum(IScoped outer, Tree.PatternVarDeclaration tree)
        {
            return tree.Initializers.Select(initializer => new PatternInitializer(outer, initializer));
        }
    }
}
