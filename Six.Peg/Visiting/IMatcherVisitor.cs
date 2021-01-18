using SixPeg.Matchers;

namespace SixPeg.Visiting
{
    public interface IMatcherVisitor<T>
    {
        T Visit(MatchRule matcher);
        T Visit(MatchReference matcher);

        T Visit(MatchNot matcher);
        T Visit(MatchAnd matcher);
        T Visit(MatchBefore matcher);

        T Visit(MatchCharacterAny matcher);
        T Visit(MatchCharacterExact matcher);
        T Visit(MatchCharacterRange matcher);
        T Visit(MatchCharacterSequence matcher);
        T Visit(MatchCharacterSet matcher);

        T Visit(MatchTerminal matcher);

        T Visit(MatchSequence matcher);
        T Visit(MatchChoice matcher);
        T Visit(MatchEpsilon matcher);
        T Visit(MatchError matcher);
        T Visit(MatchOneOrMore matcher);
        T Visit(MatchZeroOrMore matcher);
        T Visit(MatchZeroOrOne matcher);
    }
}
