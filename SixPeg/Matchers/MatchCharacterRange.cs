using Six.Support;

namespace SixPeg.Matchers
{
    public class MatchCharacterRange : AnyMatcher
    {
        public MatchCharacterRange(bool spaced, char minCharacter, char maxCharacter)
            : base(spaced)
        {
            MinCharacter = minCharacter;
            MaxCharacter = maxCharacter;
        }

        public char MinCharacter { get; }
        public char MaxCharacter { get; }

        public override bool Match(string subject, ref int cursor)
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
            writer.WriteLine($"match \"{MinCharacter}\" .. \"{MaxCharacter}\"");
        }
    }
}
