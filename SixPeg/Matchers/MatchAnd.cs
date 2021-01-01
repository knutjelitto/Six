using SixPeg.Matches;
using SixPeg.Visiting;
using System.Collections.Generic;
using System.Linq;

namespace SixPeg.Matchers
{
    public sealed class MatchAnd : BaseMatcher
    {
        public MatchAnd(AnyMatcher matcher)
            : base("&", "and", matcher)
        {
        }

        protected override IEnumerable<IMatch> InnerMatches(Context subject, int before, int start)
        {
            if (Matcher.Matches(subject, start).Materialize().Any())
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
            var start = cursor;
            if (Matcher.Match(subject, ref cursor))
            {
                cursor = start;
                return true;
            }

            cursor = start;
            return false;
        }

        protected override IMatch InnerMatch(Context subject, int before, int start)
        {
            var cursor = start;
            if (Matcher.Match(subject, ref cursor))
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
