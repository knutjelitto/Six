namespace SixComp
{
    public partial class ParseTree
    {
        public class ExpressionCondition : BaseExpression, ICondition
        {
            private ExpressionCondition(IExpression expression)
            {
                Expression = expression;
            }

            public IExpression Expression { get; }

            public static ExpressionCondition? TryParse(Parser parser)
            {
                var expression = IExpression.TryParse(parser);

                if (expression == null)
                {
                    return null;
                }

                return new ExpressionCondition(expression);
            }

            public override IExpression? LastExpression
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
}