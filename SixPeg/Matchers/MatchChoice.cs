using System.Collections.Generic;

namespace SixPeg.Matchers
{
    public class MatchChoice : BaseMatchers
    {
        public MatchChoice(IEnumerable<IMatcher> matchers)
            : base("choice", matchers)
        {
        }

        protected override bool InnerMatch(string subject, ref int cursor)
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
