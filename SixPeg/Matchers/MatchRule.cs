using Six.Support;
using SixPeg.Expression;

namespace SixPeg.Matchers
{
    public class MatchRule : AnyMatcher
    {
        public MatchRule(Identifier name, IMatcherSource matcher)
        {
            Name = name;
            Matcher = matcher;
        }

        public Identifier Name { get; }
        public IMatcherSource Matcher { get; private set; }

        public override bool Match(string subject, ref int cursor)
        {
            return Matcher.GetMatcher().Match(subject, ref cursor);
        }

        public override void Write(IWriter writer)
        {
            using (writer.Indent($"rule `{Name}`"))
            {
                Matcher.GetMatcher().Write(writer);
            }
        }

        public override string ToString()
        {
            return $"{Name.Text}";
        }
    }
}
