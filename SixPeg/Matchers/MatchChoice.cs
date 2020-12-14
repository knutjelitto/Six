using Six.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixPeg.Matchers
{
    public class MatchChoice : AnyMatcher
    {
        public MatchChoice(IEnumerable<IMatcherSource> matchers)
        {
            Matchers = matchers.ToArray();
        }

        public IReadOnlyList<IMatcherSource> Matchers { get; private set; }

        public override bool Match(string subject, ref int cursor)
        {
            var start = cursor;
            foreach (var matcher in Matchers)
            {
                if (matcher.GetMatcher().Match(subject, ref cursor))
                {
                    return true;
                }
            }
            cursor = start;
            return false;
        }

        public override void Write(IWriter writer)
        {
            using (writer.Indent("choice"))
            {
                foreach (var matcher in Matchers)
                {
                    matcher.GetMatcher().Write(writer);
                }
            }
        }
    }
}
