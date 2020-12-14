using System.Collections.Generic;
using System.Linq;

namespace SixPeg.Matchers
{
    public class MatchSequence : BaseMatchers
    {
        private MatchSequence(bool spaced, IEnumerable<IMatcher> matchers)
            : base("sequence", spaced, matchers)
        {
        }

        public static IMatcher From(bool spaced, IEnumerable<IMatcher> ms)
        {
            var matchers = ms.ToList();

            if (matchers.Count == 0)
            {
                return new MatchEpsilon(spaced);
            }
            if (matchers.Count == 1)
            {
                return matchers[0];
            }
            if (matchers.Count == 2)
            {
                    return new MatchPrefixed(spaced, matchers[0], matchers[1]);
            }

            return new MatchSequence(spaced, matchers);
        }

        public override bool Match(string subject, ref int cursor)
        {
            var start = cursor;
            foreach (var matcher in Matchers)
            {
                if (!matcher.Match(subject, ref cursor))
                {
                    cursor = start;
                    return false;
                }
            }

            return true;
        }
    }
}
