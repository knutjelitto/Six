using Six.Support;
using System;

namespace SixPeg.Matchers
{
    public abstract class BaseMatcher : AnyMatcher
    {
        private readonly Lazy<bool> isTerminal;

        public BaseMatcher(string marker, string kind, IMatcher matcher)
        {
            Marker = marker;
            Kind = kind;
            Matcher = matcher;
            isTerminal = new Lazy<bool>(() => Matcher.IsTerminal);
        }

        public override string Marker { get; }
        public string Kind { get; }
        public IMatcher Matcher { get; }

        public sealed override void Write(IWriter writer)
        {
            using (writer.Indent(SpacePrefix + Kind))
            {
                Matcher.Write(writer);
            }
        }

        public override string DDLong => $"{Kind}({Matcher.DDLong})";
    }
}
