using System.Collections.Generic;

namespace SixPeg.Matchers
{
    public class MatchChoice : BaseMatchers
    {
        public MatchChoice(bool spaced, IEnumerable<IMatcher> matchers)
            : base("choice", spaced, matchers)
        {
        }

        public override bool Match(string subject, ref int cursor)
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
