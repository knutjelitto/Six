using SixPeg.Visiting;

namespace Six.Peg.Expression
{
    public class TerminalExpression : Rule
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
