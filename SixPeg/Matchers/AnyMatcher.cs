using Six.Support;

namespace SixPeg.Matchers
{
    public abstract class AnyMatcher : IMatcher
    {
        public AnyMatcher(bool spaced)
        {
            Spaced = spaced;
        }

        protected bool Spaced { get; }
        public abstract bool Match(string subject, ref int cursor);
        public abstract void Write(IWriter writer);
    }
}
