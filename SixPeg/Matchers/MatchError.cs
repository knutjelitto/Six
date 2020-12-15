using Six.Support;

namespace SixPeg.Matchers
{
    public class MatchError : AnyMatcher
    {
        public MatchError()
        {
        }

        protected override bool InnerMatch(string subject, ref int cursor)
        {
            throw new System.NotImplementedException();
        }

        public override void Write(IWriter writer)
        {
            writer.WriteLine($"{SpacePrefix}#error(...)");
        }
    }
}
