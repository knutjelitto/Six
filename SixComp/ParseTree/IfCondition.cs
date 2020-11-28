using SixComp.Support;
using System;

namespace SixComp.ParseTree
{
    public class IfCondition : BaseExpression, AnyCondition
    {
        private IfCondition(AnyExpression expression)
        {
            Expression = expression;
        }

        public AnyExpression Expression { get; }

        public static IfCondition Parse(Parser parser)
        {
            parser.Consume(ToKind.KwIf);

            var expression = AnyExpression.TryParse(parser) ??  throw new InvalidOperationException($"{typeof(IfCondition)}");

            return new IfCondition(expression);
        }

        public override string ToString()
        {
            return $"{ToKind.KwIf.GetRep()} {Expression}";
        }
    }
}
