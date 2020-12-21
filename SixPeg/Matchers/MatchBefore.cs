using System.Diagnostics;

namespace SixPeg.Matchers
{
    public class MatchBefore : BaseMatcher
    {
        public MatchBefore(IMatcher matcher)
            : base("before", matcher)
        {
            Debug.Assert(matcher.IsClassy);
        }

        public override bool IsPredicate => true;

        protected override bool InnerMatch(Context subject, ref int cursor)
        {
            var before = cursor - 1;
            return before >= 0 && Matcher.Match(subject, ref before);
        }
    }
}
