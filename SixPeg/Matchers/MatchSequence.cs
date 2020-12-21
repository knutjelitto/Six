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
            var matchers = ms.ToList();

            if (matchers.Count == 0)
            {
                return new MatchEpsilon();
            }
            if (matchers.Count == 1)
            {
                return matchers[0];
            }

            var first = matchers.First();

            if (first is MatchSpace space)
            {
                var rest = From(matchers.Skip(1));
                //Debug.Assert(!rest.IsPredicate);
                rest.Space = space.Matcher;
                return rest;
            }

            return new MatchSequence(matchers);
        }

        protected override bool InnerMatch(Context subject, ref int cursor)
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
