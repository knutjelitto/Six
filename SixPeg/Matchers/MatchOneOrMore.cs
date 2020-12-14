namespace SixPeg.Matchers
{
    public class MatchOneOrMore : BaseMatcher
    {
        public MatchOneOrMore(bool spaced, IMatcher matcher)
            : base("one-or-more", spaced, matcher)
        {
        }

        public override bool Match(string subject, ref int cursor)
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
