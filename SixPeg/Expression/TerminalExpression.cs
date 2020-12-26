namespace SixPeg.Expression
{
    public class TerminalExpression : AnyRule
    {
        public TerminalExpression(Symbol name, AnyExpression expression)
            : base(name, expression, true)
        {
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
