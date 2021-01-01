using Six.Support;
using SixPeg.Matches;
using SixPeg.Visiting;
using System.Collections.Generic;

namespace SixPeg.Matchers
{
    public class MatchCharacterExact : MatchClassy
    {
        public MatchCharacterExact(char character)
        {
            Character = character;
        }

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

        protected override IMatch InnerMatch(Context subject, int before, int start)
        {
            if (start < subject.Length && subject.Text[start] == Character)
            {
                return IMatch.Success(this, before, start, start + 1);
            }
            return null;
        }

        public override string DDLong => $"single(\"{Character.Escape()}\")";

        public override string Marker => "=";

        public override T Accept<T>(IMatcherVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
