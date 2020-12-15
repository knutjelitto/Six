namespace SixPeg.Matchers
{
    public class MatchNot : BaseMatcher
    {
        public MatchNot(IMatcher matcher)
            : base("not", matcher)
        {
        }

        protected override bool InnerMatch(string subject, ref int cursor)
        {
            var start = cursor;
            if (Matcher.Match(subject, ref cursor))
            {
                cursor = start;
                return false;
            }

            cursor = start;
            return true;
        }
    }
}
