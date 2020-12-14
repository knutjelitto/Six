using Six.Support;

namespace SixPeg.Matchers
{
    public class MatchPrefixed : AnyMatcher
    {
        public MatchPrefixed(IMatcherSource prefix, IMatcherSource matcher)
        {
            Prefix = prefix;
            Matcher = matcher;
        }

        public IMatcherSource Prefix { get; private set; }
        public IMatcherSource Matcher { get; private set; }

        public override bool Match(string subject, ref int cursor)
        {
            var start = cursor;
            if (Prefix.GetMatcher().Match(subject, ref cursor))
            {
                if (Matcher.GetMatcher().Match(subject, ref cursor))
                {
                    return true;
                }
            }
            cursor = start;
            return false;
        }

        public override void Write(IWriter writer)
        {
            Matcher.GetMatcher().Write(writer);
            using (writer.Indent())
            {
                using (writer.Indent("prefixed-by"))
                {
                    Prefix.GetMatcher().Write(writer);
                }
            }
        }

        public override IMatcher Optimize()
        {
            Prefix = Prefix.GetMatcher().Optimize();
            Matcher = Matcher.GetMatcher().Optimize();
            return this;
        }
    }
}
