using Six.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class ArrayLiteralExpression : Items<IExpression, ParseTree.ArrayLiteral>, IExpression
    {
        public ArrayLiteralExpression(IScoped outer, ParseTree.ArrayLiteral tree)
            : base(outer, tree, Enum(outer, tree))
        {
        }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.ArrayLit);
        }

        private static IEnumerable<IExpression> Enum(IScoped outer, ParseTree.ArrayLiteral tree)
        {
            return tree.Select(expression => IExpression.Build(outer, expression));
        }
    }
}
