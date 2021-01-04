using System.Collections.Generic;
using System.Linq;

namespace SixPeg.Runtime
{
    public class Match
    {
        public static readonly IReadOnlyList<Match> NoMatches = new Match[] { };

        public Match(int start, int next, IReadOnlyList<Match> matches)
        {
            Start = start;
            Next = next;
            Matches = matches;
        }

        public int Start { get; }
        public int Next { get; }
        public IReadOnlyList<Match> Matches { get; }

        public static Match Success(int start)
        {
            return new Match(start, start, NoMatches);
        }

        public static Match Success(int start, int next)
        {
            return new Match(start, next, NoMatches);
        }

        public static Match Success(int start, Match match)
        {
            return new Match(start, match.Next, new[] { match });
        }

        public static Match Success(int start, IReadOnlyList<Match> matches)
        {
            var next = matches.LastOrDefault()?.Next ?? start;
            return new Match(start, next, matches);
        }

        public static Match Success(int start, params Match[] matches)
        {
            return new Match(start, matches.Last().Next, matches);
        }

        public static Match Optional(int start, Match match)
        {
            if (match == null)
            {
                return Success(start);
            }
            return Success(start, match);
        }
    }
}
