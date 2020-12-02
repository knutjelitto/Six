using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class BaseNames : Items<BaseName, Tree.ArgumentNameClause>
    {
        public BaseNames(IScoped outer, Tree.ArgumentNameClause tree)
            : base(outer, tree, Enum(outer, tree))
        {
        }

        private static IEnumerable<BaseName> Enum(IScoped outer, Tree.ArgumentNameClause tree)
        {
            return tree.Names.Select(n => new BaseName(outer, n.Name));
        }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.Names);
        }
    }
}
