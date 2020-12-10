namespace SixComp
{
    public partial class ParseTree
    {
        public class SubscriptExpression : PostfixExpression
        {
            private SubscriptExpression(IExpression left, Token op, SubscriptClause subscript)
                : base(left, op)
            {
                Subscript = subscript;
            }

            public SubscriptClause Subscript { get; }

            public static SubscriptExpression Parse(Parser parser, IExpression left)
            {
                var op = parser.CurrentToken;

                var subscript = SubscriptClause.Parse(parser);

                return new SubscriptExpression(left, op, subscript);
            }

            public override string ToString()
            {
                return $"{Left}{Subscript}";
            }
        }
    }
}