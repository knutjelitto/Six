using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class PatternLet : Items<PatternInitializer, Tree.LetDeclaration>, IDeclaration
    {
        public PatternLet(IScoped outer, Tree.LetDeclaration tree)
            : base(outer, tree, Enum(outer, tree))
        {
        }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.Let);
        }

        private static IEnumerable<PatternInitializer> Enum(IScoped outer, Tree.LetDeclaration tree)
        {
            return tree.Initializers.Select(initializer => new PatternInitializer(outer, initializer));
        }
    }
}
