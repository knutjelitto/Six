namespace SixComp
{
    public partial class Tree
    {
        public class NestedExpression : BaseExpression, AnyPrimaryExpression
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

            public override string ToString()
            {
                return $"({Expression})";
            }
        }
    }
}