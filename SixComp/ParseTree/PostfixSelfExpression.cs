namespace SixComp.ParseTree
{
    public class PostfixSelfExpression : PostfixExpression
    {
        public PostfixSelfExpression(AnyExpression left) : base(left) { }

        public static PostfixSelfExpression Parse(Parser parser, AnyExpression left)
        {
            parser.Consume(ToKind.Dot);
            parser.Consume(ToKind.KwSelf);

            return new PostfixSelfExpression(left);
        }
    }
}
