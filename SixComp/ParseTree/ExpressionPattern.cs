namespace SixComp.ParseTree
{
    public class ExpressionPattern : AnyPattern
    {
        private ExpressionPattern(AnyExpression? expression, AnyPattern? pattern)
        {
            Expression = expression;
            Pattern = pattern;
        }

        public AnyExpression? Expression { get; }
        public AnyPattern? Pattern { get; }

        public static ExpressionPattern Parse(Parser parser)
        {
            var expression = AnyExpression.Parse(parser);

            return new ExpressionPattern(expression, null);
        }
    }
}
