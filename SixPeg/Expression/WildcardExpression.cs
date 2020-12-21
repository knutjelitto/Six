using SixPeg.Matchers;

namespace SixPeg.Expression
{
    public class WildcardExpression : AnyExpression
    {
        public WildcardExpression()
        {
        }

        protected override IMatcher MakeMatcher()
        {
            return new MatchCharacterAny();
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
