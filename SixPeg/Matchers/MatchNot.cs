using SixPeg.Matches;
using System.Collections.Generic;
using System.Linq;

namespace SixPeg.Matchers
{
    public class MatchNot : BaseMatcher
    {
        public MatchNot(IMatcher matcher)
            : base("!", "not", matcher)
        {
        }

        protected override IEnumerable<IMatch> InnerMatches(Context subject, int before, int start)
        {
            if (Matcher.Matches(subject, start).Materialize().Any())
            {
                yield break;
            }
            else
            {
                yield return IMatch.Success(this, before, start);
            }
        }

        protected override bool InnerMatch(Context subject, ref int cursor)
        {
            var start = cursor;
            if (Matcher.Match(subject, ref cursor))
            {
                cursor = start;
                return false;
            }

            cursor = start;
            return true;
        }
    }
}
