using SixComp.Support;
using System;

namespace SixComp
{
    public partial class ParseTree
    {
        public class IfCondition : BaseExpression, ICondition
        {
            private IfCondition(IExpression expression)
            {
                Expression = expression;
            }

            public IExpression Expression { get; }

            public static IfCondition Parse(Parser parser)
            {
                parser.Consume(ToKind.KwIf);

                var expression = IExpression.TryParse(parser) ?? throw new InvalidOperationException($"{typeof(IfCondition)}");

                return new IfCondition(expression);
            }

            public override string ToString()
            {
                return $"{ToKind.KwIf.GetRep()} {Expression}";
            }
        }
    }
}