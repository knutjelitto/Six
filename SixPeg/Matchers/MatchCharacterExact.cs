using Six.Support;
using SixPeg.Matches;
using System.Collections.Generic;

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

        protected override IEnumerable<IMatch> InnerMatches(Context subject, int before, int start)
        {
            var next = start;
            if (InnerMatch(subject, ref next))
            {
                yield return IMatch.Success(this, before, start, next);
            }
            else
            {
                yield break;
            }
        }

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

        public override string DDLong => $"single(\"{Character.Escape()}\")";

        public override string Marker => "=";
    }
}
