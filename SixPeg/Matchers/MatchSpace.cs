using SixPeg.Expression;
using SixPeg.Matches;

namespace SixPeg.Matchers
{
    public class MatchSpace : MatchName
    {
        public MatchSpace(Symbol name, MatchCache matchCache, MatchesCache matchesCache)
            : base(name, matchCache, matchesCache)
        {
        }
    }
}
