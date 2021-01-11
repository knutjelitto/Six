using SixPeg.Visiting;

namespace Six.Peg.Expression
{
    public class BeforeExpression : AnyExpression
    {
        public BeforeExpression(AnyExpression expression)
        {
            Expression = expression;
        }

        public AnyExpression Expression { get; }

        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
