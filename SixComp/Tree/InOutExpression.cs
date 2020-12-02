namespace SixComp.Tree
{
    public class InOutExpression : BaseExpression, AnyPrefixExpression
    {
        private InOutExpression(AnyExpression expression)
        {
            Expression = expression;
        }

        public AnyExpression Expression { get; }

        public static InOutExpression From(AnyExpression expression)
        {
            return new InOutExpression(expression);
        }

        public override string ToString()
        {
            return $"&{Expression}";
        }
    }
}
