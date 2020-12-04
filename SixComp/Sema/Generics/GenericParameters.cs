using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class GenericParameters : Items<GenericParameter>
    {
        public GenericParameters(IWhere where, Tree.GenericParameterClause parameters)
            : base(where, Enum(where, parameters))
        {
        }

        private static IEnumerable<GenericParameter> Enum(IWhere where, Tree.GenericParameterClause tree)
        {
            return tree.Parameters.Select(p => new GenericParameter(where, p));
        }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.GenericParameters);
        }
    }
}
