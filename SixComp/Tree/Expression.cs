using System.Linq;

namespace SixComp.Tree
{
    public class Expression : BaseExpression
    {
        public Expression(TryOperator tryOp, AnyPrefixExpression left, BinaryExpressionList binaries)
        {
            TryOp = tryOp;
            Left = left;
            Binaries = binaries;
        }

        public TryOperator TryOp { get; }
        public AnyPrefixExpression Left { get; }
        public BinaryExpressionList Binaries { get; }

        public override AnyExpression? LastExpression
        {
            get
            {
                return Binaries.LastOrDefault()?.Right.LastExpression ?? Left.LastExpression;
            }
        }

        public static Expression? TryParse(Parser parser, bool withBinaries = true)
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

            return new Expression(tryOp, left, binaries);
        }

        public override string ToString()
        {
            return $"{TryOp}{Left}{Binaries}";
        }
    }
}
