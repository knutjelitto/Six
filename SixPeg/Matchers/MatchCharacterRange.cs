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

        public override bool IsClassy => true;

        protected override bool InnerMatch(Context subject, ref int cursor)
        {
            if (cursor < subject.Length && MinCharacter <= subject.Text[cursor] && subject.Text[cursor] <= MaxCharacter)
            {
                cursor += 1;
                return true;
            }
            return false;
        }

        public override void Write(IWriter writer)
        {
            writer.WriteLine($"{SpacePrefix}match \"{MinCharacter.Escape()}\" .. \"{MaxCharacter.Escape()}\"");
        }

        public override string DDShort => $"range(\"{MinCharacter.Escape()}\",\"{MaxCharacter.Escape()}\")";
    }
}
