using System;

namespace SixComp
{
    public partial class ParseTree
    {
        public class Initializer : BaseExpression, IExpression
        {
            public Initializer(IExpression expression)
            {
                Expression = expression;
            }

            public IExpression Expression { get; }

            public override IExpression? LastExpression => Expression.LastExpression;

            public static Initializer Parse(Parser parser)
            {
                parser.Consume(ToKind.Assign);

                var expression = IExpression.TryParse(parser) ?? throw new InvalidOperationException($"{typeof(Initializer)}");

                return new Initializer(expression);
            }

            public override string ToString()
            {
                return $" = {Expression}";
            }
        }
    }
}