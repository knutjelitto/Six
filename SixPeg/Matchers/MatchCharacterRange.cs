using Six.Support;

namespace SixPeg.Matchers
{
    public class MatchCharacterRange : AnyMatcher
    {
        public MatchCharacterRange(char minCharacter, char maxCharacter)
        {
            MinCharacter = minCharacter;
            MaxCharacter = maxCharacter;
        }

        public char MinCharacter { get; }
        public char MaxCharacter { get; }

        protected override bool InnerMatch(string subject, ref int cursor)
        {
            if (cursor < subject.Length && MinCharacter <= subject[cursor] && subject[cursor] <= MaxCharacter)
            {
                cursor += 1;
                return true;
            }
            return false;
        }

        public override void Write(IWriter writer)
        {
            writer.WriteLine($"{SpacePrefix}match \"{MinCharacter}\" .. \"{MaxCharacter}\"");
        }

        public override string DShort => $"range(\"{MinCharacter}\",\"{MaxCharacter}\")";
    }
}
