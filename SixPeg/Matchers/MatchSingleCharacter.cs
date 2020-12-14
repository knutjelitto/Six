using Six.Support;

namespace SixPeg.Matchers
{
    public class MatchSingleCharacter : AnyMatcher
    {
        public MatchSingleCharacter(bool spaced, char character)
            : base(spaced)
        {
            Character = character;
        }

        public char Character { get; }

        public override bool Match(string subject, ref int cursor)
        {
            if (cursor < subject.Length && subject[cursor] == Character)
            {
                cursor += 1;
                return true;
            }
            return false;
        }

        public override void Write(IWriter writer)
        {
            writer.WriteLine($"match \"{Character}\"");
        }
    }
}
