using Six.Support;
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

        public IMatcher Space { get; set; } = null;
        public bool IsClassy { get; set; } = false;
        public string SpacePrefix => Space == null ? string.Empty : "_ ";
        public virtual string DDLong => ToString();
        public abstract string Marker { get; }

        public static void Clear()
        {
            furthestCursor = 0;
        }

        public IEnumerable<IMatch> Matches(Context subject, int start)
        {
            if (this is MatchRef name)
            {
                if (name.Name.Text == "raw_string_lit")
                {
                    //new Error(subject).Report($"{name.Name.Text}", start);
                    Debug.Assert(true);
                }
            }

            var before = start;
            ConsumeSpace(subject, ref start);

            var matches = InnerMatches(subject, before, start).Materialize();

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
        public abstract T Accept<T>(IMatcherVisitor<T> visitor);
    }
}
