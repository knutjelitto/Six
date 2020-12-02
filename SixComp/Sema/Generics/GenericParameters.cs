using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class GenericParameters : Items<GenericParameter>
    {
        public GenericParameters(IOwner owner, Tree.GenericParameterClause parameters)
            : base(owner, Enum(owner, parameters))
        {
            Owner = owner;
        }

        public IOwner Owner { get; }

        private static IEnumerable<GenericParameter> Enum(IOwner owner, Tree.GenericParameterClause tree)
        {
            return tree.Parameters.Select(p => new GenericParameter(owner, p));
        }

        public override void Report(IWriter writer)
        {
            if (Count > 0)
            {
                this.ReportList(writer, Strings.Head.GenericParameters);
            }
        }
    }
}
