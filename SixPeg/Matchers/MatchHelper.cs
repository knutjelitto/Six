using SixPeg.Matches;
using System.Collections.Generic;
using System.Diagnostics;

namespace SixPeg.Matchers
{
    public static class MatchHelper
    {
        [DebuggerStepThrough]
        public static IEnumerable<IMatch> Materialize(this IEnumerable<IMatch> matches)
        {
            //return matches.ToList();
            return matches;
        }
    }
}
