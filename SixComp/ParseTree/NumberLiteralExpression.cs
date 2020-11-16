namespace SixComp.ParseTree
{
    public sealed class NumberLiteralExpression : AnyLiteralExpression
    {
        public NumberLiteralExpression(Token token) : base(token)
        {
        }

        public static NumberLiteralExpression Parse(Parser parser)
        {
            var token = parser.Consume(ToKind.Number);

            return new NumberLiteralExpression(token);
        }
    }
}
