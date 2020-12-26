using SixPeg.Matches;
using System.Collections.Generic;

namespace SixPeg.Matchers
{
    public class MatchZeroOrOne : BaseMatcher
    {
        public MatchZeroOrOne(IMatcher matcher)
            : base("?", "zero-or-one", matcher)
        {
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
    }
}
