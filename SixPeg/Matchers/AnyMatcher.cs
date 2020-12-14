using Six.Support;
using SixPeg.Expression;

namespace SixPeg.Matchers
{
    public abstract class AnyMatcher : IMatcher
    {
        public IMatcher Matcher => Expression.GetMatcher();

        public abstract bool Match(string subject, ref int cursor);
        public abstract void Write(IWriter writer);
    }
}
