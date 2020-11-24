using System;

namespace SixComp.ParseTree
{
    public class Initializer : BaseExpression, AnyExpression
    {
        public Initializer(AnyExpression expression)
        {
            Expression = expression;
        }

        public AnyExpression Expression { get; }

        public override AnyExpression? LastExpression => Expression.LastExpression;

        public static Initializer Parse(Parser parser)
        {
            parser.Consume(ToKind.Assign);

            var expression = AnyExpression.TryParse(parser) ?? throw new InvalidOperationException();

            return new Initializer(expression);
        }

        public override string ToString()
        {
            return $" = {Expression}";
        }
    }
}
