namespace SixComp.Tree
{
    public class ExpressionCondition : BaseExpression, AnyCondition
    {
        private ExpressionCondition(AnyExpression expression)
        {
            Expression = expression;
        }

        public AnyExpression Expression { get; }

        public static ExpressionCondition? TryParse(Parser parser)
        {
            var expression = AnyExpression.TryParse(parser);

            if (expression == null)
            {
                return null;
            }

            return new ExpressionCondition(expression);
        }

        public override AnyExpression? LastExpression
        {
            get
            {
                var last = Expression.LastExpression;
                return last;
            }
        }

        public override string ToString()
        {
            return $"{Expression}";
        }
    }
}
