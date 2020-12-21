using Six.Support;

namespace SixPeg.Matchers
{
    public abstract class BaseMatcher : AnyMatcher
    {
        public BaseMatcher(string kind, IMatcher matcher)
        {
            Kind = kind;
            Matcher = matcher;
        }

        public string Kind { get; }
        public IMatcher Matcher { get; }

        public sealed override void Write(IWriter writer)
        {
            using (writer.Indent(SpacePrefix + Kind))
            {
                Matcher.Write(writer);
            }
        }

        public override string DDShort => $"{Kind}({Matcher.DDShort})";
        public override string DDLong => $"{Kind}({Matcher.DDLong})";
    }
}
