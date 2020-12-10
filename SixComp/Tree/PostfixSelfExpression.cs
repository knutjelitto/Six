namespace SixComp
{
    public partial class ParseTree
    {
        public class PostfixSelfExpression : PostfixExpression
        {
            private PostfixSelfExpression(IExpression left, Token op, Token self)
                : base(left, op)
            {
                Self = BaseName.From(self);
            }

            public BaseName Self { get; }

            public static PostfixSelfExpression Parse(Parser parser, IExpression left)
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
}