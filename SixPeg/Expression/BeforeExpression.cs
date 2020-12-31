using SixPeg.Matchers;
using SixPeg.Visiting;

namespace SixPeg.Expression
{
    public class BeforeExpression : AnyExpression
    {
        public BeforeExpression(AnyExpression expression)
        {
            Expression = expression;
        }

        public AnyExpression Expression { get; }

        protected override AnyMatcher MakeMatcher()
        {
            return new MatchBefore(Expression.GetMatcher());
        }

        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
