using SixPeg.Visiting;

namespace Six.Peg.Expression
{
    public class SpacedExpression : AnyExpression
    {
        public SpacedExpression(Symbol name)
        {
            Name = name;
        }

        public Symbol Name { get; }
        public AnyExpression Expression { get; internal set; }

        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
