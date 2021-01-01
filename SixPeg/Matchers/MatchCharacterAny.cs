using SixPeg.Matches;
using SixPeg.Visiting;
using System.Collections.Generic;

namespace SixPeg.Matchers
{
    public sealed class MatchCharacterAny : MatchClassy
    {
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
            if (cursor < subject.Length)
            {
                cursor += 1;
                return true;
            }
            return false;
        }

        protected override IMatch InnerMatch(Context subject, int before, int start)
        {
            if (start < subject.Length)
            {
                return IMatch.Success(this, before, start, start + 1);
            }
            return null;
        }

        public override T Accept<T>(IMatcherVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string DDLong => $"any(.)";

        public override string Marker => ".";
    }
}
