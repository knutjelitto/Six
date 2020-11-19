namespace SixComp.ParseTree
{
    public class SubscriptExpression : PostfixExpression
    {
        public SubscriptExpression(AnyExpression left, SubscriptClause subscript)
            : base(left)
        {
            Subscript = subscript;
        }

        public SubscriptClause Subscript { get; }

        public static SubscriptExpression Parse(Parser parser, AnyExpression left)
        {
            var subscript = SubscriptClause.Parse(parser);

            return new SubscriptExpression(left, subscript);
        }
    }
}
