using SixPeg.Visiting;

namespace SixPeg.Expression
{
    public class QuantifiedExpression : AnyExpression
    {
        public QuantifiedExpression(AnyExpression expression, Quantifier quantifier)
        {
            Expression = expression;
            Quantifier = quantifier;
        }

        public AnyExpression Expression { get; }
        public Quantifier Quantifier { get; }

        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
