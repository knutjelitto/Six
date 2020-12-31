using Six.Support;
using SixPeg.Expression;
using SixPeg.Matches;
using SixPeg.Visiting;
using System.Collections.Generic;
using System.Diagnostics;

namespace SixPeg.Matchers
{
    public class MatchRef : AnyMatcher
    {
        private readonly HashSet<string> terminals = new HashSet<string>
        {
            "identifier",
            "int_lit",
            "float_lit",
            "imaginary_lit",
            "rune_lit",
            "string_lit",
            "raw_string_lit",
            "interpreted_string_lit",
            "decimal_lit",
            "binary_lit",
            "octal_lit",
            "hex_lit",
            "decimal_digits",
            "binary_digits",
            "octal_digits",
            "hex_digits",
            "int_lit",
            "';?",
        };

        public MatchRef(Symbol name, MatchCache matchCache, MatchesCache matchesCache)
        {
            Name = name;
            MatchCache = matchCache;
            MatchesCache = matchesCache;
        }

        public Symbol Name { get; }
        public MatchCache MatchCache { get; }
        public MatchesCache MatchesCache { get; }

        public IMatcher Matcher { get; private set; } = null;
        public override string Marker => $"{Name}";

        public void SetMatcher(IMatcher matcher)
        {
            Debug.Assert(matcher != null);
            Matcher = matcher;
        }

        protected override IEnumerable<IMatch> InnerMatches(Context subject, int before, int start)
        {
            if (!MatchesCache.Already(start, out var cached))
            {
                var matches = new List<IMatch>();
                foreach (var match in Matcher.Matches(subject, start).Materialize())
                {
                    var named = IMatch.Success(this, before, start, match);
                    matches.Add(named);
                    yield return named;

                    if (terminals.Contains(Name.Text))
                    {
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

        public override string DDLong => $"{Name}";

        public override T Accept<T>(IMatcherVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
