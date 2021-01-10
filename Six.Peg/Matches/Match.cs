using SixPeg.Matchers;
using System.Collections.Generic;
using System.Diagnostics;

namespace SixPeg.Matches
{
    public class Match : IMatch
    {
        [DebuggerStepThrough]
        public Match(IMatcher matcher, int before, int start, int next, IReadOnlyList<IMatch> matches)
        {
            Matcher = matcher;
            Before = before;
            Start = start;
            Next = next;
            Matches = matches;

            if (Next > AnyMatcher.furthestCursor)
            {
                AnyMatcher.furthestCursor = Next;
            }
        }

        public IMatcher Matcher { get; }
        public int Before { get; }
        public int Start { get; }
        public int Next { get; }
        public IReadOnlyList<IMatch> Matches { get; }

        public override string ToString()
        {
            return $"{Matcher.Marker} {Before},{Start},{Next} <<{Matches.Count}>>";
        }
    }
}
