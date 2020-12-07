using SixComp.Entities;
using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class GenericParameters : Items<GenericParameter>
    {
        public GenericParameters(IWithRestrictions where, Tree.GenericParameterClause parameters)
            : base(where, Enum(where, parameters))
        {
        }

        public override void Resolve(IWriter writer)
        {
            // TODO: nothing to resolve here?
        }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.GenericParameters);
        }

        private static IEnumerable<GenericParameter> Enum(IWithRestrictions where, Tree.GenericParameterClause tree)
        {
            return tree.Parameters.Select(p => new GenericParameter(where, p));
        }
    }
}
