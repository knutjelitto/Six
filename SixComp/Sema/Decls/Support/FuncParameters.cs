using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class FuncParameters : Items<FuncParameter, Tree.ParameterClause>
    {
        public FuncParameters(IScoped outer, Tree.ParameterClause tree)
            : base(outer, tree, Enum(outer, tree))
        {
        }

        public override void Resolve(IWriter writer)
        {
            ResolveItems(writer);
        }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.Parameters);
        }

        private static IEnumerable<FuncParameter> Enum(IScoped outer, Tree.ParameterClause tree)
        {
            return tree.Parameters.Select(p => new FuncParameter(outer, p));
        }
    }
}
