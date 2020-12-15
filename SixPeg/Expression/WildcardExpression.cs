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
            return new MatchAnyCharacter();
        }

        protected override void InnerResolve()
        {
        }
    }
}
