using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class ArrayLiteral : Items<IExpression>, IExpression
    {
        public ArrayLiteral(IScoped outer, Tree.ArrayLiteral tree)
            : base(outer, Enum(outer, tree))
        {
            Tree = tree;
        }

        public Tree.ArrayLiteral Tree { get; }

        private static IEnumerable<IExpression> Enum(IScoped outer, Tree.ArrayLiteral tree)
        {
            return tree.Select(expression => IExpression.Build(outer, expression));
        }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.ArrayLit);
        }
    }
}
