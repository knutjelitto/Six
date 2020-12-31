using SixPeg.Matches;
using SixPeg.Visiting;
using System.Collections.Generic;
using System.Diagnostics;

namespace SixPeg.Matchers
{
    public sealed class MatchBefore : BaseMatcher
    {
        public MatchBefore(AnyMatcher matcher)
            : base("<", "before", matcher)
        {
            Debug.Assert(matcher.IsClassy);
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

        public override T Accept<T>(IMatcherVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
