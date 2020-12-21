using SixPeg.Expression;
using SixPeg.Matches;
using System;

namespace SixPeg.Matchers
{
    public class MatchSpace : MatchName
    {
        public MatchSpace(Symbol name, MatchCache cache, Func<IMatcher> makeMatcher)
            : base(name, cache, makeMatcher)
        {
        }
    }
}
