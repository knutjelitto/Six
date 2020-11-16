namespace SixComp.ParseTree
{
    public class ArrayLiteralItem : AnyExpression
    {
        public ArrayLiteralItem(AnyExpression expression)
        {
            Expression = expression;
        }

        public AnyExpression Expression { get; }

        public static ArrayLiteralItem Parse(Parser parser)
        {
            var expression = AnyExpression.Parse(parser);

            return new ArrayLiteralItem(expression);
        }

        public override string ToString()
        {
            return $"{Expression}";
        }
    }
}
