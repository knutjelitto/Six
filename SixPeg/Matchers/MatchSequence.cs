using Six.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixPeg.Matchers
{
    public class MatchSequence : AnyMatcher
    {
        public MatchSequence(IEnumerable<IMatcher> matchers)
        {
            Matchers = matchers.ToArray();
        }

        public IReadOnlyList<IMatcher> Matchers { get; private set; }

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

        public override void Write(IWriter writer)
        {
            using (writer.Indent("sequence"))
            {
                foreach (var matcher in Matchers)
                {
                    matcher.Write(writer);
                }
            }
        }

        public override IMatcher Optimize()
        {
            if (Matchers.Count == 2 && Matchers[0] is MatchPrefixed)
            {
                return new MatchPrefixed(Matchers[0].Optimize(), Matchers[1].Optimize());
            }
            Matchers = Matchers.Select(m => m.Optimize()).ToArray();
            return this;
        }
    }
}
