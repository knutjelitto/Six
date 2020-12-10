namespace SixComp
{
    public partial class ParseTree
    {
        public class InOutExpression : BaseExpression, IPrefixExpression
        {
            private InOutExpression(IExpression expression)
            {
                Expression = expression;
            }

            public IExpression Expression { get; }

            public static InOutExpression From(IExpression expression)
            {
                return new InOutExpression(expression);
            }

            public override string ToString()
            {
                return $"&{Expression}";
            }
        }
    }
}