using Six.Support;

namespace SixPeg.Matchers
{
    public class MatchCharacterExact : AnyMatcher
    {
        public MatchCharacterExact(char character)
        {
            Character = character;
        }

        public override bool IsClassy => true;

        public char Character { get; }

        protected override bool InnerMatch(Context subject, ref int cursor)
        {
            if (cursor < subject.Length && subject.Text[cursor] == Character)
            {
                cursor += 1;
                return true;
            }
            return false;
        }

        public override void Write(IWriter writer)
        {
            writer.WriteLine($"{SpacePrefix}match \"{Character.Escape()}\"");
        }

        public override string DDShort => $"single(\"{Character.Escape()}\")";
    }
}
