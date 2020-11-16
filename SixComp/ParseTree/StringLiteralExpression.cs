namespace SixComp.ParseTree
{
    public class StringLiteralExpression : AnyLiteralExpression
    {
        public StringLiteralExpression(Token token) : base(token)
        {
        }

        public static StringLiteralExpression Parse(Parser parser)
        {
            var token = parser.Consume(ToKind.String);

            return new StringLiteralExpression(token);
        }
    }
}
