using SixPeg.Visiting;

namespace Six.Peg.Expression
{
    public class RuleExpression : Rule
    {
        public RuleExpression(Symbol name, Attributes attributes, AnyExpression expression)
            : base(name, attributes, expression, false)
        {
        }

        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
