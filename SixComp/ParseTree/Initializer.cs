namespace SixComp.ParseTree
{
    public class Initializer : AnyExpression
    {
        public Initializer(AnyExpression expression)
        {
            Expression = expression;
        }

        public AnyExpression Expression { get; }

        public static Initializer Parse(Parser parser)
        {
            parser.Consume(ToKind.Equal);

            var expression = AnyExpression.Parse(parser);

            return new Initializer(expression);
        }

        public override string ToString()
        {
            return $" = {Expression}";
        }
    }
}
