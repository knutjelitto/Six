using SixPeg.Matches;
using SixPeg.Runtime;
using SixPeg.Visiting;
using System.Collections.Generic;

namespace SixPeg.Matchers
{
    public sealed class MatchBefore : BaseMatcher
    {
        public MatchBefore(AnyMatcher matcher)
            : base("<", "before", matcher)
        {
        }

        protected override IEnumerable<IMatch> InnerMatches(Context subject, int before, int start)
        {
            if (InnerMatch(subject, ref start))
            {
                yield return IMatch.Success(this, before, start);
            }
            else
            {
                yield break;
            }
        }

        protected override bool InnerMatch(Context subject, ref int cursor)
        {
            var before = cursor - 1;
            return before >= 0 && Matcher.Match(subject, ref before);
        }

        protected override IMatch InnerMatch(Context subject, int before, int start)
        {
            var cursor = start - 1;
            if (cursor >= 0 && Matcher.Match(subject, ref cursor))
            {
                return IMatch.Success(this, before, start);
            }
            return null;
        }

        public override T Accept<T>(IMatcherVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
