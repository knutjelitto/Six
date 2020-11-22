namespace SixComp.ParseTree
{
    public class NestedExpression : BaseExpression, AnyPrimary
    {
        private NestedExpression(AnyExpression expression)
        {
            Expression = expression;
        }

        public AnyExpression Expression { get; }

        public static NestedExpression From(AnyExpression expression)
        {
            return new NestedExpression(expression);
        }
    }
}
