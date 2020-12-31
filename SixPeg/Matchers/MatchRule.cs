using Six.Support;
using SixPeg.Expression;
using SixPeg.Matches;
using SixPeg.Visiting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SixPeg.Matchers
{
    public class MatchRule : AnyMatcher
    {
        public MatchRule(Symbol name)
        {
            Name = name;

            MatchCache = new MatchCache(Name);
            MatchesCache = new MatchesCache(Name);
        }

        public override string Marker => $"{Name}=";
        public override string DDLong => $"{Name}={Matcher?.DDLong ?? string.Empty}";

        public bool IsTerminal { get; set; } = false;
        public bool IsSingle { get; set; } = false;
        public Symbol Name { get; }
        private MatchCache MatchCache { get; }
        private MatchesCache MatchesCache { get; }

        public AnyMatcher Matcher { get; set; }

        public new void Clear()
        {
            MatchCache.Clear();
            MatchesCache.Clear();
        }

        protected override IEnumerable<IMatch> InnerMatches(Context subject, int before, int start)
        {
            Debug.Assert(Matcher != null);

            if (IsTerminal)
            {
                Debug.Assert(true);
            }
            if (Name.Text == "LPAREN_NEW")
            {
                //new Error(subject).Report($"{Name.Text}", start);
                Debug.Assert(true);
            }

            if (!MatchesCache.Already(start, out var cached))
            {
                var matches = new List<IMatch>();

                if (IsTerminal)
                {
                    var next = start;
                    if (Matcher.Match(subject, ref next))
                    {
                        var named = IMatch.Success(this, before, start, next);
                        matches.Add(named);
                        yield return named;
                    }
                }
                else
                {
                    foreach (var match in Matcher.Matches(subject, start).Materialize())
                    {
                        var named = IMatch.Success(this, before, start, match);
                        matches.Add(named);
                        yield return named;

                        if (IsSingle)
                        {
                            break;
                        }
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
            Debug.Assert(Matcher != null);

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
            throw new NotImplementedException();
        }

        public override T Accept<T>(IMatcherVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
