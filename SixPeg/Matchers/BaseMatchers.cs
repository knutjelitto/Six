using Six.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixPeg.Matchers
{
    public abstract class BaseMatchers : AnyMatcher
    {
        public BaseMatchers(string kind, IEnumerable<IMatcher> matchers)
        {
            Kind = kind;
            Matchers = matchers;
        }

        public string Kind { get; }
        public IEnumerable<IMatcher> Matchers { get; }

        public override void Write(IWriter writer)
        {
            using (writer.Indent(SpacePrefix + Kind))
            {
                foreach (var matcher in Matchers)
                {
                    matcher.Write(writer);
                }
            }
        }

        public override string DDShort => $"{Kind}(...)";
        public override string DDLong => $"{Kind}({string.Join(",", Matchers.Select(m => m.DDShort))})";
    }
}
