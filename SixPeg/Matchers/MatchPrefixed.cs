using Six.Support;

namespace SixPeg.Matchers
{
    public class MatchPrefixed : AnyMatcher
    {
        public MatchPrefixed(IMatcher prefix, IMatcher matcher)
        {
            Prefix = prefix;
            Matcher = matcher;
        }

        public IMatcher Prefix { get; private set; }
        public IMatcher Matcher { get; private set; }

        protected override bool InnerMatch(string subject, ref int cursor)
        {
            var start = cursor;
            if (Prefix.Match(subject, ref cursor))
            {
                if (Matcher.Match(subject, ref cursor))
                {
                    return true;
                }
            }
            cursor = start;
            return false;
        }

        public override void Write(IWriter writer)
        {
            Matcher.Write(writer);
            using (writer.Indent())
            {
                using (writer.Indent("{SpacePrefix}prefixed-by"))
                {
                    Prefix.Write(writer);
                }
            }
        }
    }
}