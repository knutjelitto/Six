namespace SixComp.ParseTree
{
    public class SubscriptExpression : PostfixExpression
    {
        private SubscriptExpression(AnyExpression left, Token op, SubscriptClause subscript)
            : base(left, op)
        {
            Subscript = subscript;
        }

        public SubscriptClause Subscript { get; }

        public static SubscriptExpression Parse(Parser parser, AnyExpression left)
        {
            var op = parser.CurrentToken;

            var subscript = SubscriptClause.Parse(parser);

            return new SubscriptExpression(left, op, subscript);
        }
    }
}
