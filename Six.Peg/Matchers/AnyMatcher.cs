using Six.Peg.Runtime;
using SixPeg.Matches;
using SixPeg.Visiting;
using System.Collections.Generic;
using System.Diagnostics;

namespace SixPeg.Matchers
{
    [DebuggerDisplay("{DDLong}")]
    public abstract class AnyMatcher : IMatcher, IVisitableMatcher
    {
        public static int furthestCursor = 0;

        public bool UsedByRule { get; set; } = false;
        public bool UsedByTerminal { get; set; } = false;
        public bool Used => UsedByRule || UsedByTerminal;
        public bool Fragment => !UsedByRule;
        public bool IsFragment { get; set; } = false;
        public bool IsPredicate { get; set; } = false;
        public bool AlwaysSucceeds { get; set; } = false;
        public bool NeverContinues { get; set; } = false;

        public IMatcher Space { get; set; } = null;
        public virtual bool IsClassy { get; } = false;
        public string SpacePrefix => Space == null ? string.Empty : "_ ";
        public virtual string DDLong => ToString();
        public abstract string Marker { get; }

        public static void Clear()
        {
            furthestCursor = 0;
        }

        public IEnumerable<IMatch> Matches(Context subject, int start)
        {
            var before = start;
            ConsumeSpace(subject, ref start);

            var matches = InnerMatches(subject, before, start).Materialize();

            return matches;
        }

        public bool Match(Context subject, ref int cursor)
        {
            var start = cursor;
            ConsumeSpace(subject, ref cursor);

            var match = InnerMatch(subject, ref cursor);
            if (!match)
            {
                cursor = start;
            }

            return match;
        }

        public IMatch Match(Context subject, int start)
        {
            var before = start;
            ConsumeSpace(subject, ref start);

            var match = InnerMatch(subject, before, start);

            return match;
        }

        protected abstract IEnumerable<IMatch> InnerMatches(Context subject, int before, int start);
        protected abstract bool InnerMatch(Context subject, ref int cursor);
        protected abstract IMatch InnerMatch(Context subject, int before, int start);

        [DebuggerStepThrough]
        private void ConsumeSpace(Context subject, ref int cursor)
        {
            if (Space != null)
            {
                _ = Space.Match(subject, ref cursor);
            }
        }

        public abstract T Accept<T>(IMatcherVisitor<T> visitor);
    }
}
