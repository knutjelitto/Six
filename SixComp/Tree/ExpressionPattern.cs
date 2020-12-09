using System;

namespace SixComp
{
    public partial class Tree
    {
        public class ExpressionPattern : SyntaxNode, AnyPattern
        {
            private ExpressionPattern(AnyExpression expression)
            {
                Expression = expression;
            }

            public AnyExpression Expression { get; }

            public static ExpressionPattern Parse(Parser parser)
            {
                var expression = AnyExpression.TryParse(parser) ?? throw new InvalidOperationException($"{typeof(ExpressionPattern)}");

                return new ExpressionPattern(expression);
            }

            public override string ToString()
            {
                return $"{Expression}";
            }
        }
    }
}