using Six.Support;
using SixPeg.Expression;
using System.Diagnostics;

namespace SixPeg.Matchers
{
    public class MatchName : AnyMatcher
    {
        public MatchName(Identifier name, IMatcher matcher)
        {
            Name = name;
            Matcher = matcher;
        }

        public Identifier Name { get; }
        public IMatcher Matcher { get; }

        protected override bool InnerMatch(string subject, ref int cursor)
        {
            if (Name.Text == "identifier")
            {
                Debug.Assert(true);
            }
            return Matcher.Match(subject, ref cursor);
        }

        public override void Write(IWriter writer)
        {
            writer.WriteLine($"{SpacePrefix}ref {Name}");
        }

        public override string ToString()
        {
            return $"{Name}";
        }

        public override string DShort => $"{Name}";
    }
}
