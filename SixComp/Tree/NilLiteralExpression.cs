namespace SixComp.Tree
{
    public class NilLiteralExpression : AnyLiteralExpression
    {
        public NilLiteralExpression(Token token) : base(token) { }

        public static NilLiteralExpression Parse(Parser parser)
        {
            var token = parser.Consume(ToKind.KwNil);

            return new NilLiteralExpression(token);
        }
    }
}
