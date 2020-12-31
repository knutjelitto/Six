using Six.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixPeg.Matchers
{
    public abstract class BaseMatchers : AnyMatcher
    {
        public BaseMatchers(string marker, string kind, IEnumerable<AnyMatcher> matchers)
        {
            Marker = marker;
            Kind = kind;
            Matchers = matchers.ToArray();
        }

        public override string Marker { get; }
        public string Kind { get; }
        public IReadOnlyList<AnyMatcher> Matchers { get; }

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

        public override string DDLong => $"{Kind}({string.Join(",", Matchers.Select(m => m.DDLong))})";
    }
}
