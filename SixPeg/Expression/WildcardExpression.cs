using SixPeg.Matchers;

namespace SixPeg.Expression
{
    public class WildcardExpression : AnyExpression
    {
        public WildcardExpression()
        {
        }

        public override IMatcher GetMatcher(bool spaced)
        {
            return new MatchAnyCharacter(spaced);
        }

        public override void Resolve(GrammarExpression grammar)
        {
        }
    }
}
