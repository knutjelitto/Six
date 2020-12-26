using Six.Support;
using SixPeg.Matches;
using System.Collections.Generic;
using System.Diagnostics;

namespace SixPeg.Matchers
{
    [DebuggerDisplay("{DDLong}")]
    public abstract class AnyMatcher : IMatcher
    {
        public static int furthestCursor = 0;
        public static int furthestFail = 0;
        public static int furthestSuccess = 0;

        public IMatcher Space { get; set; } = null;
        public bool IsTerminal { get; set; } = false;
        protected string SpacePrefix => Space == null ? string.Empty : "_ ";
        public virtual string DDLong => ToString();
        public abstract string Marker { get; }
        public virtual bool IsClassy => false;

        public static void Clear()
        {
            furthestCursor = 0;
            furthestFail = 0;
            furthestSuccess = 0;
        }

        public IEnumerable<IMatch> Matches(Context subject, int cursor)
        {
            var before = cursor;
            ConsumeSpace(subject, ref cursor);

            var matches = InnerMatches(subject, before, cursor).Materialize();

            foreach (var match in matches)
            {
                if (match.Next > furthestCursor)
                {
                    furthestCursor = match.Next;
                }
            }

            return matches;
        }

        protected abstract IEnumerable<IMatch> InnerMatches(Context subject, int before, int start);

        public bool Match(Context subject, ref int cursor)
        {
            var start = cursor;
            ConsumeSpace(subject, ref cursor);
            var match = InnerMatch(subject, ref cursor);
            if (!match)
            {
                cursor = start;
            }

            if (cursor > furthestCursor)
            {
                furthestCursor = cursor;
            }
            if (match && cursor > furthestSuccess)
            {
                furthestSuccess = cursor;
            }
            if (!match && cursor > furthestFail)
            {
                furthestFail = cursor;
            }

            return match;
        }

        [DebuggerStepThrough]
        private void ConsumeSpace(Context subject, ref int cursor)
        {
            if (Space != null)
            {
                _ = Space.Match(subject, ref cursor);
            }
        }

        protected abstract bool InnerMatch(Context subject, ref int cursor);
        public abstract void Write(IWriter writer);
    }
}
