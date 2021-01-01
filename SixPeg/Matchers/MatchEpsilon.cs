using SixPeg.Matches;
using SixPeg.Visiting;
using System.Collections.Generic;

namespace SixPeg.Matchers
{
    public class MatchEpsilon : AnyMatcher
    {

        public MatchEpsilon()
        {
        }

        public override string Marker => "ε";

        protected override IEnumerable<IMatch> InnerMatches(Context subject, int before, int start)
        {
            yield return IMatch.Success(this, before, start);
        }

        protected override bool InnerMatch(Context subject, ref int cursor)
        {
            return true;
        }

        protected override IMatch InnerMatch(Context subject, int before, int start)
        {
            return IMatch.Success(this, before, start);
        }

        public override T Accept<T>(IMatcherVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
