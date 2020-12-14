using Six.Support;

namespace SixPeg.Matchers
{
    public abstract class BaseMatcher : AnyMatcher
    {
        public BaseMatcher(string kind, bool spaced, IMatcher matcher)
            : base(spaced)
        {
            Kind = kind;
            Matcher = matcher;
        }

        public string Kind { get; }
        public IMatcher Matcher { get; }

        public sealed override void Write(IWriter writer)
        {
            using (writer.Indent(Kind))
            {
                Matcher.Write(writer);
            }
        }

    }
}
