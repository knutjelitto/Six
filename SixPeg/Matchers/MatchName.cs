using Six.Support;
using SixPeg.Expression;

namespace SixPeg.Matchers
{
    public class MatchName : AnyMatcher
    {
        public MatchName(Identifier name, bool spaced, IMatcher matcher)
            : base(spaced)
        {
            Name = name;
            Matcher = matcher;
        }

        public Identifier Name { get; }
        public IMatcher Matcher { get; }

        public override bool Match(string subject, ref int cursor)
        {
            return Matcher.Match(subject, ref cursor);
        }

        public override void Write(IWriter writer)
        {
            writer.WriteLine($"reference <{Name}>");
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
