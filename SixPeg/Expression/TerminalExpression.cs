using SixPeg.Visiting;

namespace SixPeg.Expression
{
    public class TerminalExpression : AnyRule
    {
        public TerminalExpression(Symbol name, Attributes attributes, AnyExpression expression)
            : base(name, attributes, expression, true)
        {
        }

        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
