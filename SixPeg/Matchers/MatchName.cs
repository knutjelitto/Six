using Six.Support;
using SixPeg.Expression;
using SixPeg.Matches;
using System;
using System.Diagnostics;

namespace SixPeg.Matchers
{
    public class MatchName : AnyMatcher
    {
        private IMatcher matcher = null;

        public MatchName(Symbol name, MatchCache cache, Func<IMatcher> makeMatcher)
        {
            Name = name;
            Cache = cache;
            MakeMatcher = makeMatcher;
        }

        public Symbol Name { get; }
        public MatchCache Cache { get; }
        public Func<IMatcher> MakeMatcher { get; }
        public IMatcher Matcher => matcher ??= MakeMatcher();
        public override bool IsClassy => Matcher.IsClassy;

        protected override bool InnerMatch(Context subject, ref int cursor)
        {
            if (Name.Text == "TypeSwitchGuard")
            {
                new Error(subject).Report($"{Name.Text}", cursor);
                Debug.Assert(true);

            }
            if (!Cache.Already(cursor, out var cached))
            {
                var start = cursor;
                var result = Matcher.Match(subject, ref cursor);
                Cache.Cache(start, (result, cursor));
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

        public override string DDShort => $"{Name}";
    }
}
