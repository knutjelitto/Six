using SixPeg.Matches;
using System.Collections.Generic;
using System.Diagnostics;

namespace SixPeg.Matchers
{
    public class MatchBefore : BaseMatcher
    {
        public MatchBefore(IMatcher matcher)
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
    }
}
