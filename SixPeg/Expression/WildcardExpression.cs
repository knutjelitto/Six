using SixPeg.Matchers;
using SixPeg.Visiting;

namespace SixPeg.Expression
{
    public class WildcardExpression : AnyExpression
    {
        public WildcardExpression()
        {
        }

        protected override AnyMatcher MakeMatcher()
        {
            return new MatchCharacterAny();
        }

        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
