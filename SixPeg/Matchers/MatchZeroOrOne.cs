namespace SixPeg.Matchers
{
    public class MatchZeroOrOne : BaseMatcher
    {
        public MatchZeroOrOne(IMatcher matcher)
            : base("zero-or-one", matcher)
        {
        }

        protected override bool InnerMatch(Context subject, ref int cursor)
        {
            return Matcher.Match(subject, ref cursor) || true;
        }
    }
}
