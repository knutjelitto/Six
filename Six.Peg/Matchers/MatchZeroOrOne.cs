using SixPeg.Matches;
using Six.Peg.Runtime;
using SixPeg.Visiting;
using System.Collections.Generic;

namespace SixPeg.Matchers
{
    public class MatchZeroOrOne : BaseMatcher
    {
        public MatchZeroOrOne(AnyMatcher matcher)
            : base("?", "zero-or-one", matcher)
        {
            AlwaysSucceeds = true;
        }

        protected override IEnumerable<IMatch> InnerMatches(Context subject, int before, int start)
        {
            var have = false;
            foreach (var match in Matcher.Matches(subject, start).Materialize())
            {
                yield return IMatch.Success(this, before, start, match);
                have = true;
            }
            if (!have)
            {
                yield return IMatch.Success(this, before, start);
            }
        }

        protected override bool InnerMatch(Context subject, ref int cursor)
        {
            return Matcher.Match(subject, ref cursor) || true;
        }

        protected override IMatch InnerMatch(Context subject, int before, int start)
        {
            var match = Matcher.Match(subject, start);
            if (match == null)
            {
                return IMatch.Success(this, before, start);
            }
            return IMatch.Success(this, before, start, match);
        }

        public override T Accept<T>(IMatcherVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
