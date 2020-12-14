using Six.Support;

namespace SixPeg.Matchers
{
    public class MatchEpsilon : AnyMatcher
    {
        public override bool Match(string subject, ref int cursor)
        {
            return true;
        }

        public override void Write(IWriter writer)
        {
            writer.WriteLine($"epsilon");
        }
    }
}
