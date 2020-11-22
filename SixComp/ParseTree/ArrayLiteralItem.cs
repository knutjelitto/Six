using System;

namespace SixComp.ParseTree
{
    public class ArrayLiteralItem : BaseExpression, AnyExpression
    {
        public ArrayLiteralItem(AnyExpression expression)
        {
            Expression = expression;
        }

        public AnyExpression Expression { get; }

        public static ArrayLiteralItem Parse(Parser parser)
        {
            var expression = AnyExpression.TryParse(parser) ?? throw new InvalidOperationException();

            return new ArrayLiteralItem(expression);
        }

        public override string ToString()
        {
            return $"{Expression}";
        }
    }
}
