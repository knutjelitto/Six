namespace SixComp.ParseTree
{
    public class ExpressionCondition : AnyCondition
    {
        private ExpressionCondition(AnyExpression expression)
        {
            Expression = expression;
        }

        public AnyExpression Expression { get; }

        public static ExpressionCondition Parse(Parser parser)
        {
            var expression = AnyExpression.Parse(parser);

            return new ExpressionCondition(expression);
        }

        public override string ToString()
        {
            return $"{Expression}";
        }
    }
}
