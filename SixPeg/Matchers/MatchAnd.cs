using SixPeg.Matches;
using System.Collections.Generic;
using System.Linq;

namespace SixPeg.Matchers
{
    public class MatchAnd : BaseMatcher
    {
        public MatchAnd(IMatcher matcher)
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
    }
}
