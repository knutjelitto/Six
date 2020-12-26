using Six.Support;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SixPeg.Matchers
{
    public abstract class BaseMatchers : AnyMatcher
    {
        private readonly Lazy<bool> isTerminal;

        public BaseMatchers(string marker, string kind, IEnumerable<IMatcher> matchers)
        {
            Marker = marker;
            Kind = kind;
            Matchers = matchers.ToArray();
            isTerminal = new Lazy<bool>(() => Matchers.All(m => m.IsTerminal));
        }

        public override string Marker { get; }
        public string Kind { get; }
        public IReadOnlyList<IMatcher> Matchers { get; }

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
