using Six.Support;

namespace SixPeg.Matchers
{
    public class MatchCharacterAny : AnyMatcher
    {
        public MatchCharacterAny()
        {
        }

        public override bool IsClassy => true;

        protected override bool InnerMatch(Context subject, ref int cursor)
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
            writer.WriteLine($"{SpacePrefix}match any");
        }

        public override string DDShort => $"any(.)";
    }
}
