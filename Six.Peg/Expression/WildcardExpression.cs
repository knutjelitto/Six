using SixPeg.Visiting;

namespace SixPeg.Expression
{
    public class WildcardExpression : AnyExpression
    {
        public WildcardExpression()
        {
        }

        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
