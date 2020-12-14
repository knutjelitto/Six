namespace SixPeg.Matchers
{
    public class MatchAnd : BaseMatcher
    {
        public MatchAnd(bool spaced, IMatcher matcher)
            : base("and", spaced, matcher)
        {
        }

        public override bool Match(string subject, ref int cursor)
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
