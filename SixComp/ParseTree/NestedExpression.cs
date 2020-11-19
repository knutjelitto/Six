namespace SixComp.ParseTree
{
    public class NestedExpression : AnyPrimary
    {
        private NestedExpression(AnyExpression expression)
        {
            Expression = expression;
        }

        public AnyExpression Expression { get; }

        public static NestedExpression Parse(Parser parser)
        {
            parser.Consume(ToKind.LParent);
            var expression = AnyExpression.Parse(parser);
            parser.Consume(ToKind.RParent);

            return new NestedExpression(expression);
        }

        public static NestedExpression From(AnyExpression expression)
        {
            return new NestedExpression(expression);
        }
    }
}
