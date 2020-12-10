namespace SixComp
{
    public partial class ParseTree
    {
        public class NestedExpression : BaseExpression, IPrimaryExpression
        {
            private NestedExpression(IExpression expression)
            {
                Expression = expression;
            }

            public IExpression Expression { get; }

            public static NestedExpression From(IExpression expression)
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