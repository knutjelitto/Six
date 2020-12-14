using Six.Support;
using System.Collections.Generic;

namespace SixPeg.Matchers
{
    public abstract class BaseMatchers : AnyMatcher
    {
        public BaseMatchers(string kind, bool spaced, IEnumerable<IMatcher> matchers)
            : base(spaced)
        {
            Kind = kind;
            Matchers = matchers;
        }

        public string Kind { get; }
        public IEnumerable<IMatcher> Matchers { get; }


        public override void Write(IWriter writer)
        {
            using (writer.Indent(Kind))
            {
                foreach (var matcher in Matchers)
                {
                    matcher.Write(writer);
                }
            }
        }

    }
}
