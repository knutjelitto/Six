using System.Collections.Generic;
using System.Linq;

namespace SixPeg.Matchers
{
    public class MatchChoice : BaseMatchers
    {
        public MatchChoice(IEnumerable<IMatcher> matchers)
            : base("choice", matchers)
        {
        }

        public override bool IsClassy => Matchers.All(m => m.IsClassy);

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
