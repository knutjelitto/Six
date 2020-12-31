using SixPeg.Visiting;

namespace SixPeg.Expression
{
    public class RuleExpression : AnyRule
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
