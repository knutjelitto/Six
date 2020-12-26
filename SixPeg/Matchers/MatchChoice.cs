using SixPeg.Matches;
using System.Collections.Generic;
using System.Linq;

namespace SixPeg.Matchers
{
    public class MatchChoice : BaseMatchers
    {
        public MatchChoice(IEnumerable<IMatcher> matchers)
            : base("/", "choice", matchers)
        {
        }

        public override bool IsClassy => Matchers.All(m => m.IsClassy);

        protected override IEnumerable<IMatch> InnerMatches(Context subject, int before, int start)
        {
            foreach (var matcher in Matchers)
            {
                foreach (var match in matcher.Matches(subject, start).Materialize())
                {
                    yield return match;
                }
            }
        }

        protected override bool InnerMatch(Context subject, ref int cursor)
        {
            var start = cursor;
            foreach (var matcher in Matchers)
            {
                if (matcher.Match(subject, ref cursor))
                {
                    return true;
                }
            }
            cursor = start;
            return false;
        }
    }
}
