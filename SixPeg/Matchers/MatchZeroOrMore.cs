using Six.Support;

namespace SixPeg.Matchers
{
    public class MatchZeroOrMore : AnyMatcher
    {
        public MatchZeroOrMore(IMatcher matcher)
        {
            Matcher = matcher;
        }

        public IMatcher Matcher { get; private set; }

        public override bool Match(string subject, ref int cursor)
        {
            while (Matcher.Match(subject, ref cursor))
            {
                ;
            }

            return true;
        }


        public override void Write(IWriter writer)
        {
            using (writer.Indent("zero-or-more"))
            {
                Matcher.Write(writer);
            }
        }
    }
}
