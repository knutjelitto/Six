namespace SixComp.ParseTree
{
    public class PostfixSelfExpression : PostfixExpression
    {
        private PostfixSelfExpression(AnyExpression left, Token op, Token self) : base(left, op)
        {
            Self = self;
        }

        public Token Self { get; }

        public static PostfixSelfExpression Parse(Parser parser, AnyExpression left)
        {
            var op = parser.Consume(ToKind.Dot);
            var self = parser.Consume(ToKind.KwSelf);

            return new PostfixSelfExpression(left, op, self);
        }

        public override string ToString()
        {
            return $"{base.ToString()}{Self}";
        }
    }
}
