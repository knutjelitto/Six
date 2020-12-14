using Six.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class Inheritance : Items<ITypeDefinition>
    {
        public Inheritance(IScoped outer, ParseTree.TypeInheritanceClause tree)
            : base(outer, Enum(outer, tree))
        {
        }

        public Inheritance(IScoped outer)
            : base(outer)
        {
        }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.Inherits);
        }

        private static IEnumerable<ITypeDefinition> Enum(IScoped outer, ParseTree.TypeInheritanceClause tree)
        {
            return tree.Types.Select(type => ITypeDefinition.Build(outer, type));
        }
    }
}
