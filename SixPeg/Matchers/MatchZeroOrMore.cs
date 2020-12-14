namespace SixPeg.Matchers
{
    public class MatchZeroOrMore : BaseMatcher
    {
        public MatchZeroOrMore(bool spaced, IMatcher matcher)
            : base("zero-or-more", spaced, matcher)
        {
        }

        public override bool Match(string subject, ref int cursor)
        {
            while (Matcher.Match(subject, ref cursor))
            {
                ;
            }

            return true;
        }
    }
}
