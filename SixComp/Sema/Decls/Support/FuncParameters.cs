using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class FuncParameters : Items<FuncParameter, ParseTree.ParameterClause>
    {
        public FuncParameters(IScoped outer, ParseTree.ParameterClause tree)
            : base(outer, tree, Enum(outer, tree))
        {
        }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.Parameters);
        }

        private static IEnumerable<FuncParameter> Enum(IScoped outer, ParseTree.ParameterClause tree)
        {
            return tree.Parameters.Select(p => new FuncParameter(outer, p));
        }
    }
}
