using Six.Support;
using SixPeg.Matches;
using SixPeg.Runtime;
using SixPeg.Visiting;
using System.Collections.Generic;
using System.Diagnostics;

namespace SixPeg.Matchers
{
    public class MatchCharacterSet : AnyMatcher
    {
        public MatchCharacterSet(string set)
        {
            Debug.Assert(set.Length >= 2);
            Set = set;
        }

        public string Set { get; }

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
            if (cursor < subject.Length && Set.Contains(subject.Text[cursor]))
            {
                cursor += Set.Length;
                return true;
            }

            return false;
        }

        protected override IMatch InnerMatch(Context subject, int before, int start)
        {
            if (start < subject.Length && Set.Contains(subject.Text[start]))
            {
                return IMatch.Success(this, before, start, start + 1);
            }
            return null;
        }

        public override string DDLong => $"string(\"{Set.Escape()}\")";
        public override string Marker => "\"..\"";

        public override T Accept<T>(IMatcherVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
