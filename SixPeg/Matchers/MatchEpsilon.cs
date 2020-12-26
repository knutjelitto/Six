using Six.Support;
using SixPeg.Matches;
using System.Collections.Generic;

namespace SixPeg.Matchers
{
    public class MatchEpsilon : AnyMatcher
    {

        public MatchEpsilon()
        {
        }

        public override string Marker => "ε";

        protected override IEnumerable<IMatch> InnerMatches(Context subject, int before, int start)
        {
            yield return IMatch.Success(this, before, start);
        }

        protected override bool InnerMatch(Context subject, ref int cursor)
        {
            return true;
        }

        public override void Write(IWriter writer)
        {
            writer.WriteLine($"{SpacePrefix}ε");
        }
    }
}
