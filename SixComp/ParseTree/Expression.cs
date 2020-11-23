namespace SixComp.ParseTree
{
    public class Expression : BaseExpression
    {
        public Expression(TryOperator tryOp, AnyPrefix prefix, BinaryExpressionList binaries)
        {
            TryOp = tryOp;
            Prefix = prefix;
            Binaries = binaries;
        }

        public TryOperator TryOp { get; }
        public AnyPrefix Prefix { get; }
        public BinaryExpressionList Binaries { get; }

        public static Expression? TryParse(Parser parser)
        {
            var offset = parser.Offset;

            var tryOp = TryOperator.Parse(parser);

            var left = AnyPrefix.TryParse(parser);

            if (left == null)
            {
                parser.Offset = offset;
                return null;
            }

            var binaries = BinaryExpressionList.Parse(parser);

            return new Expression(tryOp, left, binaries);
        }
    }
}
