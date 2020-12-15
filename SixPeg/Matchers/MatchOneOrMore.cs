namespace SixPeg.Matchers
{
    public class MatchOneOrMore : BaseMatcher
    {
        public MatchOneOrMore(IMatcher matcher)
            : base("one-or-more", matcher)
        {
        }

        protected override bool InnerMatch(string subject, ref int cursor)
        {
            var matched = false;
            while (Matcher.Match(subject, ref cursor))
            {
                matched = true;
            }

            return matched;
        }
    }
}
