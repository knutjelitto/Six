using SixPeg.Visiting;

namespace SixPeg.Expression
{
    public class NotExpression : AnyExpression
    {
        public NotExpression(AnyExpression expression)
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
