using Six.Support;

namespace SixPeg.Matchers
{
    public class MatchName : AnyMatcher
    {
        public MatchName(IMatcherSource matcher)
        {
            Matcher = matcher;
        }

        public IMatcherSource Matcher { get; private set; }

        public override bool Match(string subject, ref int cursor)
        {
            return Matcher.GetMatcher().Match(subject, ref cursor);
        }

        public override void Write(IWriter writer)
        {
            writer.WriteLine($"reference <{Matcher}>");
        }

        public override string ToString()
        {
            return $"{Matcher}";
        }
    }
}
