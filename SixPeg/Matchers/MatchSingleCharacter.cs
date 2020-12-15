using Six.Support;

namespace SixPeg.Matchers
{
    public class MatchSingleCharacter : AnyMatcher
    {
        public MatchSingleCharacter(char character)
        {
            Character = character;
        }

        public char Character { get; }

        protected override bool InnerMatch(string subject, ref int cursor)
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
            writer.WriteLine($"{SpacePrefix}match \"{Character}\"");
        }

        public override string DShort => $"single(\"{Character}\")";
    }
}
