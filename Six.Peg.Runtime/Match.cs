using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Six.Peg.Runtime
{
    public class Match
    {
        public static readonly IReadOnlyList<Match> NoMatches = new Match[] { };

        public Match(string name, int start, int next, IReadOnlyList<Match> matches)
        {
            Name = name;
            Before = start;
            Start = start;
            Next = next;
            Matches = matches;
        }

        public string Name { get; }
        public int Before { get; set; }
        public int Start { get; }
        public int Next { get; }
        public int Lenght => Next - Start;
        public IReadOnlyList<Match> Matches { get; }

        public static Match Success(string name, int start)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(name));
            return new Match(name, start, start, NoMatches);
        }

        public static Match Success(string name, Match match)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(name));
            return new Match(name, match.Start, match.Next, NoMatches);
        }

        public static Match Success(string name, int start, int next)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(name));
            return new Match(name, start, next, NoMatches);
        }

        public static Match Success(string name, int start, Match match)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(name));
            return new Match(name, start, match.Next, new[] { match });
        }

        public static Match Success(string name, int start, int next, Match match)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(name));
            return new Match(name, start, next, new[] { match });
        }

        public static Match Success(string name, int start, IReadOnlyList<Match> matches)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(name));
            var next = matches.LastOrDefault()?.Next ?? start;
            return new Match(name, start, next, matches);
        }

        public static Match Optional(int start, Match match)
        {
            if (match == null)
            {
                return Success("?", start);
            }
            return Success("?", start, match);
        }
    }
}
