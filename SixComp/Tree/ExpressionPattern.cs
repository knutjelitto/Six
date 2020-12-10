using System;

namespace SixComp
{
    public partial class ParseTree
    {
        public class ExpressionPattern : SyntaxNode, IPattern
        {
            private ExpressionPattern(IExpression expression)
            {
                Expression = expression;
            }

            public IExpression Expression { get; }

            public static ExpressionPattern Parse(Parser parser)
            {
                var expression = IExpression.TryParse(parser) ?? throw new InvalidOperationException($"{typeof(ExpressionPattern)}");

                return new ExpressionPattern(expression);
            }

            public override string ToString()
            {
                return $"{Expression}";
            }
        }
    }
}