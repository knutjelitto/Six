using Six.Peg.Runtime;
using Six.Support;
using SixPeg.Matches;
using SixPeg.Visiting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SixPeg.Matchers
{
    public class MatchCharacterSequence : AnyMatcher
    {
        public MatchCharacterSequence(string text)
        {
            Debug.Assert(text.Length >= 2);
            Text = text;
        }

        public string Text { get; }

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
            if (cursor + Text.Length <= subject.Length && MemoryExtensions.Equals(Text.AsSpan(), subject.Text.AsSpan(cursor, Text.Length), StringComparison.Ordinal))
            {
                cursor += Text.Length;
                return true;
            }

            return false;
        }

        protected override IMatch InnerMatch(Context subject, int before, int start)
        {
            if (start + Text.Length <= subject.Length && MemoryExtensions.Equals(Text.AsSpan(), subject.Text.AsSpan(start, Text.Length), StringComparison.Ordinal))
            {
                return IMatch.Success(this, before, start, start + Text.Length);
            }
            return null;
        }

        public override string DDLong => $"string(\"{Text.Escape()}\")";
        public override string Marker => "\"..\"";

        public override T Accept<T>(IMatcherVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
