﻿using Six.Peg.Expression;
using SixPeg.Matches;
using Six.Peg.Runtime;
using SixPeg.Visiting;
using System.Collections.Generic;
using System.Diagnostics;

namespace SixPeg.Matchers
{
    public class MatchRule : AnyMatcher
    {
        public MatchRule(Symbol name, bool isTerminal)
        {
            Name = name;
            IsTerminal = isTerminal;

            MatchCacheBool = new MatchCacheBool(Name);
            MatchCache = new Matches.MatchCache(Name);
            MatchesCache = new MatchesCache(Name);
        }

        public Symbol Name { get; }
        public bool IsTerminal { get; }
        public int Index { get; set; } = -1;
        public bool IsSingle { get; set; } = false;
        public bool Flatten { get; set; } = false;
        public bool Lift { get; set; } = false;
        private MatchCacheBool MatchCacheBool { get; }
        private Matches.MatchCache MatchCache { get; }
        private MatchesCache MatchesCache { get; }
        public AnyMatcher Matcher { get; set; }
        public override string Marker => $"{Name}=";
        public override string DDLong => $"{Name}={Matcher?.DDLong ?? "<not-yet>"}";


        public new void Clear()
        {
            MatchCacheBool.Clear();
            MatchCache.Clear();
            MatchesCache.Clear();
        }

        protected override IEnumerable<IMatch> InnerMatches(Context subject, int before, int start)
        {
            Debug.Assert(Matcher != null);

            if (Name.Text == "LPAREN_NEW")
            {
                //new Error(subject).Report($"{Name.Text}", start);
                Debug.Assert(true);
            }

            if (MatchesCache.Already(start, out var cached))
            {
                foreach (var match in cached)
                {
                    Debug.Assert(match.Before == before);
                    yield return match;
                }
            }
            else
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
        }

        protected override bool InnerMatch(Context subject, ref int cursor)
        {
            Debug.Assert(Matcher != null);

            if (!MatchCacheBool.Already(cursor, out var cached))
            {
                var start = cursor;
                var result = Matcher.Match(subject, ref cursor);
                MatchCacheBool.Cache(start, (result, cursor));
                return result;
            }
            else
            {
                cursor = cached.cursor;
                return cached.result;
            }
        }

        protected override IMatch InnerMatch(Context subject, int before, int start)
        {
            Debug.Assert(Matcher != null);

            if (MatchCache.Already(start, out var cached))
            {
                return cached;
            }
            else
            {
                IMatch result = null;

                if (IsTerminal)
                {
                    Debug.Assert(true);
                    var cursor = start;
                    if (Matcher.Match(subject, ref cursor))
                    {
                        result = IMatch.Success(this, before, start, cursor);
                    }
                }
                else
                {
                    result = Matcher.Match(subject, start);
                    if (result != null)
                    {
                        result = IMatch.Success(this, before, start, result);
                    }
                }
                MatchCache.Cache(start, result);
                return result;
            }
        }

        public override T Accept<T>(IMatcherVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
