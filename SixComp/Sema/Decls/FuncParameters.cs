using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class FuncParameters : Items<FuncParameter>
    {
        public FuncParameters(IOwner owner, Tree.ParameterClause tree)
            : base(owner, Enum(owner, tree))
        {
            Owner = owner;
            Tree = tree;
        }

        public IOwner Owner { get; }
        public Tree.ParameterClause Tree { get; }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.Parameters);
        }

        private static IEnumerable<FuncParameter> Enum(IOwner func, Tree.ParameterClause tree)
        {
            return tree.Parameters.Select(p => new FuncParameter(func, p));
        }
    }
}
