using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class FuncTypeParameters : Items<FuncTypeParameter>
    {
        public FuncTypeParameters(IScoped outer, Tree.FunctionTypeArgumentClause tree)
            : base(outer, Enum(outer, tree.Arguments))
        {
            Tree = tree;
        }

        public Tree.FunctionTypeArgumentClause Tree { get; }
        public bool Variadic => Tree.Variadic.Present;

        private static IEnumerable<FuncTypeParameter> Enum(IScoped outer, IEnumerable<Tree.FunctionTypeArgument> arguments)
        {
            return arguments.Select(argument => new FuncTypeParameter(outer, argument));
        }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.Parameters);
        }
    }
}
