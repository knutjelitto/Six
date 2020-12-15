using Six.Support;
using System.Diagnostics;

namespace SixPeg.Matchers
{
    [DebuggerDisplay("{DLong}")]
    public abstract class AnyMatcher : IMatcher
    {
        public IMatcher Space { get; set; } = null;
        protected abstract bool InnerMatch(string subject, ref int cursor);
        public abstract void Write(IWriter writer);

        public bool Match(string subject, ref int cursor)
        {
            ConsumeSpace(subject, ref cursor);
            var match = InnerMatch(subject, ref cursor);

            return match;
        }

        [DebuggerStepThrough]
        private void ConsumeSpace(string subject, ref int cursor)
        {
            if (Space != null)
            {
                Debug.Assert(true);
                _ = Space.Match(subject, ref cursor);
            }
        }

        protected string SpacePrefix => Space == null ? string.Empty : "_ ";

        public virtual string DShort => ToString();
        public virtual string DLong => DShort;
    }
}
