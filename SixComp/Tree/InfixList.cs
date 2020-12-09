using SixComp.Common;
using System.Linq;

namespace SixComp
{
    public partial class Tree
    {
        public class InfixList : BaseExpression
        {
            public InfixList(AnyPrefixExpression left, BinaryExpressionList binaries)
            {
                Left = left;
                Binaries = binaries;
            }

            public AnyPrefixExpression Left { get; }
            public BinaryExpressionList Binaries { get; }

            public override AnyExpression? LastExpression
            {
                get
                {
                    return Binaries.LastOrDefault()?.Right.LastExpression ?? Left.LastExpression;
                }
            }

            public static AnyExpression? TryParse(Parser parser, bool withBinaries = true)
            {
                var offset = parser.Offset;

                var tryOp = TryOperator.Parse(parser);

                var left = AnyPrefixExpression.TryParse(parser);

                if (left == null)
                {
                    parser.Offset = offset;
                    return null;
                }

                var binaries = withBinaries
                    ? BinaryExpressionList.Parse(parser)
                    : new BinaryExpressionList();

                AnyExpression expression;
                if (binaries.Count > 0)
                {
                    expression = new InfixList(left, binaries);
                }
                else
                {
                    expression = left;
                }
                if (tryOp.Kind != TryKind.None)
                {
                    return TryExpression.From(tryOp, expression);
                }
                else
                {
                    return expression;
                }
            }

            public override string ToString()
            {
                return $"{Left}{Binaries}";
            }
        }
    }
}