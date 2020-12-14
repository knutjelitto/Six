using SixPeg.Matchers;

namespace SixPeg.Expression
{
    public class WildcardExpression : AnyExpression
    {
        public WildcardExpression()
        {
        }

        public override IMatcher GetMatcher()
        {
            return new MatchAnyCharacter(this);
        }

        public override void Resolve(GrammarExpression grammar)
        {
        }
    }
}
