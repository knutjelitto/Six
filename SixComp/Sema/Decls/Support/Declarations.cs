using SixComp.Support;
using System.Collections.Generic;
using System.Diagnostics;

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
            foreach (var declaration in tree.Declarations)
            {
                if (declaration is Tree.EnumCase enumCase)
                {
                    var prefix = enumCase.Prefix;
                    Debug.Assert(prefix.IsEmpty);
                    foreach (var caseItem in enumCase.CaseItems)
                    {
                        yield return new EnumCaseDeclaration(outer, prefix, caseItem);
                    }
                }
                else
                {
                    yield return IDeclaration.Build(outer, declaration);
                }
            }
        }
    }
}
