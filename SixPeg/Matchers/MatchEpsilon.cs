using Six.Support;

namespace SixPeg.Matchers
{
    public class MatchEpsilon : AnyMatcher
    {
        public MatchEpsilon()
        {
        }

        protected override bool InnerMatch(string subject, ref int cursor)
        {
            return true;
        }

        public override void Write(IWriter writer)
        {
            writer.WriteLine($"{SpacePrefix}epsilon");
        }
    }
}
