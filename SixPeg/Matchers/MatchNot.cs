using Six.Support;

namespace SixPeg.Matchers
{
    public class MatchNot : AnyMatcher
    {
        public MatchNot(IMatcher matcher)
        {
            Matcher = matcher;
        }

        public IMatcher Matcher { get; private set; }

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

        public override void Write(IWriter writer)
        {
            using (writer.Indent("not"))
            {
                Matcher.Write(writer);
            }
        }
    }
}
