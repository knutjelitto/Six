using Six.Support;

namespace SixPeg.Matchers
{
    public class MatchZeroOrOne : AnyMatcher
    {
        public MatchZeroOrOne(bool spaced, IMatcher matcher)
            : base(spaced)
        {
            Matcher = matcher;
        }

        public IMatcher Matcher { get; private set; }

        public override bool Match(string subject, ref int cursor)
        {
            return Matcher.Match(subject, ref cursor) || true;
        }

        public override void Write(IWriter writer)
        {
            using (writer.Indent("zero-or-one"))
            {
                Matcher.Write(writer);
            }
        }
    }
}
