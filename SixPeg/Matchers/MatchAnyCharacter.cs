using Six.Support;

namespace SixPeg.Matchers
{
    public class MatchAnyCharacter : AnyMatcher
    {
        public MatchAnyCharacter(bool spaced)
            : base(spaced)
        {
        }

        public override bool Match(string subject, ref int cursor)
        {
            if (cursor < subject.Length)
            {
                cursor += 1;
                return true;
            }
            return false;
        }

        public override void Write(IWriter writer)
        {
            writer.WriteLine($"any-character");
        }
    }
}
