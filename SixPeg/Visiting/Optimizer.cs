using SixPeg.Matchers;

namespace SixPeg.Visiting
{
    public class Optimizer : IMatcherVisitor<AnyMatcher>
    {

        public AnyMatcher Optimize(IMatcher matcher)
        {
             return matcher.Accept(this);
        }

        public AnyMatcher Visit(MatchAnd matcher)
        {
            return matcher;
       }

        public AnyMatcher Visit(MatchBefore matcher)
        {
            return matcher;
        }

        public AnyMatcher Visit(MatchCharacterAny matcher)
        {
            return matcher;
        }

        public AnyMatcher Visit(MatchCharacterExact matcher)
        {
            return matcher;
        }

        public AnyMatcher Visit(MatchCharacterRange matcher)
        {
            return matcher;
        }

        public AnyMatcher Visit(MatchCharacterSequence matcher)
        {
            return matcher;
        }

        public AnyMatcher Visit(MatchChoice matcher)
        {
            return matcher;
        }

        public AnyMatcher Visit(MatchEpsilon matcher)
        {
            return matcher;
        }

        public AnyMatcher Visit(MatchError matcher)
        {
            return matcher;
        }

        public AnyMatcher Visit(MatchReference matcher)
        {
            return matcher;
        }

        public AnyMatcher Visit(MatchRule matcher)
        {
            return matcher;
        }

        public AnyMatcher Visit(MatchNot matcher)
        {
            return matcher;
        }

        public AnyMatcher Visit(MatchOneOrMore matcher)
        {
            return matcher;
        }

        public AnyMatcher Visit(MatchSequence matcher)
        {
            return matcher;
        }

        public AnyMatcher Visit(MatchZeroOrMore matcher)
        {
            return matcher;
        }

        public AnyMatcher Visit(MatchZeroOrOne matcher)
        {
            return matcher;
        }
    }
}
