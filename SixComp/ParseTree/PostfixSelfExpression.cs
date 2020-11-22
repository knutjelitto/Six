namespace SixComp.ParseTree
{
    public class PostfixSelfExpression : PostfixExpression
    {
        private PostfixSelfExpression(AnyExpression left, Token op) : base(left, op) { }

        public static PostfixSelfExpression Parse(Parser parser, AnyExpression left)
        {
            var op = parser.Consume(ToKind.Dot);
            parser.Consume(ToKind.KwSelf);

            return new PostfixSelfExpression(left, op);
        }
    }
}
