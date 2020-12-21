using Six.Support;

namespace SixPeg.Matchers
{
    public class MatchEpsilon : AnyMatcher
    {
        public MatchEpsilon()
        {
        }

        protected override bool InnerMatch(Context subject, ref int cursor)
        {
            return true;
        }

        public override void Write(IWriter writer)
        {
            writer.WriteLine($"{SpacePrefix}ε");
        }
    }
}
