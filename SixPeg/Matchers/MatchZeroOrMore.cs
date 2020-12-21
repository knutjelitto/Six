namespace SixPeg.Matchers
{
    public sealed class MatchZeroOrMore : BaseMatcher
    {
        public MatchZeroOrMore(IMatcher matcher)
            : base("zero-or-more", matcher)
        {
        }

        protected override bool InnerMatch(Context subject, ref int cursor)
        {
            while (Matcher.Match(subject, ref cursor))
            {
                ;
            }

            return true;
        }
    }
}
