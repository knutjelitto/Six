using Six.Support;

namespace SixPeg.Matchers
{
    public class MatchOneOrMore : AnyMatcher
    {
        public MatchOneOrMore(IMatcher matcher)
        {
            Matcher = matcher;
        }

        public IMatcher Matcher { get; private set; }

        public override bool Match(string subject, ref int cursor)
        {
            var matched = false;
            while (Matcher.Match(subject, ref cursor))
            {
                matched = true;
            }

            return matched;
        }

        public override void Write(IWriter writer)
        {
            using (writer.Indent("one-or-more"))
            {
                Matcher.Write(writer);
            }
        }

        public override IMatcher Optimize()
        {
            Matcher = Matcher.Optimize();
            return this;
        }
    }
}
