using System;

namespace SixComp.ParseTree
{
    public class ExpressionPattern : SyntaxNode, AnyPattern
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
            var expression = AnyExpression.TryParse(parser) ?? throw new InvalidOperationException($"{typeof(ExpressionPattern)}");

            return new ExpressionPattern(expression, null);
        }
    }
}
