namespace SixPeg.Matchers
{
    public class MatchAnd : BaseMatcher
    {
        public MatchAnd(IMatcher matcher)
            : base("and", matcher)
        {
        }

        protected override bool InnerMatch(string subject, ref int cursor)
        {
            var start = cursor;
            if (Matcher.Match(subject, ref cursor))
            {
                cursor = start;
                return true;
            }

            cursor = start;
            return false;
        }
    }
}
