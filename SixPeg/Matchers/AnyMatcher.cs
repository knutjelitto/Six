using Six.Support;
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
        protected string SpacePrefix => Space == null ? string.Empty : "_ ";
        public virtual string DDShort => ToString();
        public virtual string DDLong => DDShort;

        public virtual bool IsClassy => false;
        public virtual bool IsPredicate => false;

        public static void Clear()
        {
            furthestCursor = 0;
            furthestFail = 0;
            furthestSuccess = 0;
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
