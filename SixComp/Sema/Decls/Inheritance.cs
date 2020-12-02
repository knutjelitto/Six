using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class Inheritance : Items<IType, Tree.TypeInheritanceClause>
    {
        public Inheritance(IScoped outer, Tree.TypeInheritanceClause tree)
            : base(outer, tree, Enum(outer, tree))
        {
        }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.Inherits);
        }

        private static IEnumerable<IType> Enum(IScoped outer, Tree.TypeInheritanceClause tree)
        {
            return tree.Types.Select(type => IType.Build(outer, type));
        }
    }
}
