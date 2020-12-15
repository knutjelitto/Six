using System.Collections.Generic;
using System.Linq;

namespace SixPeg.Matchers
{
    public class MatchSequence : BaseMatchers
    {
        private MatchSequence(IEnumerable<IMatcher> matchers)
            : base("sequence", matchers)
        {
        }

        public static IMatcher From(IEnumerable<IMatcher> ms)
        {
            var mss = ms.ToList();

            if (mss.Count == 0)
            {
                return new MatchEpsilon();
            }
            if (mss.Count == 1)
            {
                return mss[0];
            }

            var first = mss.First();

            if (first is MatchName name && name.Name.Text == "_")
            {
                var rest = From(mss.Skip(1));
                rest.Space = name.Matcher;
                return rest;
            }

            return new MatchSequence(mss);
        }

        protected override bool InnerMatch(string subject, ref int cursor)
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
