using Six.Support;
using SixPeg.Expression;
using SixPeg.Matches;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SixPeg.Matchers
{
    public class MatchName : AnyMatcher
    {
        private readonly Lazy<bool> isTerminal;

        public MatchName(Symbol name, MatchCache matchCache, MatchesCache matchesCache)
        {
            Name = name;
            MatchCache = matchCache;
            MatchesCache = matchesCache;
            isTerminal = new Lazy<bool>(() => Matcher.IsTerminal);
        }

        public Symbol Name { get; }
        public MatchCache MatchCache { get; }
        public MatchesCache MatchesCache { get; }

        public IMatcher Matcher { get; private set; } = null;
        public override bool IsClassy => Matcher.IsClassy;
        public override string Marker => $"<{Name.Text}>";

        public void SetMatcher(IMatcher matcher)
        {
            Debug.Assert(matcher != null);
            Matcher = matcher;
        }

        protected override IEnumerable<IMatch> InnerMatches(Context subject, int before, int start)
        {
            if (Name.Text == "FunctionType")
            {
                //new Error(subject).Report($"{Name.Text}", start);
                Debug.Assert(true);

            }
            if (before == 505)
            {
                Debug.Assert(true);
            }
            if (!MatchesCache.Already(start, out var cached))
            {
                var matches = new List<IMatch>();
                foreach (var match in Matcher.Matches(subject, start).Materialize())
                {
                    var named = IMatch.Success(this, before, start, match);
                    matches.Add(named);
                    yield return named;

                    if (IsTerminal)
                    {
                        Console.Write($".{Name.Text}");
                        break;
                    }
                }

                MatchesCache.Cache(start, matches);
            }
            else
            {
                foreach (var match in cached)
                {
                    Debug.Assert(match.Before == before);
                    yield return match;
                }
            }
        }

        protected override bool InnerMatch(Context subject, ref int cursor)
        {
            if (!MatchCache.Already(cursor, out var cached))
            {
                var start = cursor;
                var result = Matcher.Match(subject, ref cursor);
                MatchCache.Cache(start, (result, cursor));
                return result;
            }
            else
            {
                cursor = cached.cursor;
                return cached.result;
            }
        }

        public override void Write(IWriter writer)
        {
            writer.WriteLine($"{SpacePrefix}{Name}");
        }

        public override string ToString()
        {
            return $"{Name}";
        }

        public override string DDLong => $"{Name}";
    }
}
