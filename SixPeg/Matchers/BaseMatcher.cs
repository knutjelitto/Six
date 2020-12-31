using Six.Support;

namespace SixPeg.Matchers
{
    public abstract class BaseMatcher : AnyMatcher
    {
        public BaseMatcher(string marker, string kind, AnyMatcher matcher)
        {
            Marker = marker;
            Kind = kind;
            Matcher = matcher;
        }

        public override string Marker { get; }
        public string Kind { get; }
        public AnyMatcher Matcher { get; }

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
