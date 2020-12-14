namespace SixPeg.Matchers
{
    public class MatchNot : BaseMatcher
    {
        public MatchNot(bool spaced, IMatcher matcher)
            : base("not", spaced, matcher)
        {
        }

        public override bool Match(string subject, ref int cursor)
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
