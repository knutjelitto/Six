using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class Declarations : Items<IDeclaration>
    {
        public Declarations(IScoped outer, Tree.DeclarationClause tree)
            : base(outer, Enum(outer, tree))
        {
            Tree = tree;
        }

        public Tree.DeclarationClause Tree { get; }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.Declatations);
        }

        private static IEnumerable<IDeclaration> Enum(IScoped outer, Tree.DeclarationClause tree)
        {
            return tree.Declarations.Select(declaration => IDeclaration.Build(outer, declaration));
        }
    }
}
