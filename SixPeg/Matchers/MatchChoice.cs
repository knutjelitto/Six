using SixPeg.Matches;
using SixPeg.Visiting;
using System.Collections.Generic;

namespace SixPeg.Matchers
{
    public class MatchChoice : BaseMatchers
    {
        public MatchChoice(IEnumerable<AnyMatcher> matchers)
            : base("/", "choice", matchers)
        {
        }

        protected override IEnumerable<IMatch> InnerMatches(Context subject, int before, int start)
        {
            foreach (var matcher in Matchers)
            {
                foreach (var match in matcher.Matches(subject, start).Materialize())
                {
                    yield return match;
                }
            }
        }

        protected override bool InnerMatch(Context subject, ref int cursor)
        {
            var start = cursor;
            foreach (var matcher in Matchers)
            {
                if (matcher.Match(subject, ref cursor))
                {
                    return true;
                }
            }
            cursor = start;
            return false;
        }

        public override T Accept<T>(IMatcherVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
