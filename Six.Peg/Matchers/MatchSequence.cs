using SixPeg.Matches;
using Six.Peg.Runtime;
using SixPeg.Visiting;
using System.Collections.Generic;

namespace SixPeg.Matchers
{
    public class MatchSequence : BaseMatchers
    {
        public MatchSequence(IEnumerable<AnyMatcher> matchers)
            : base("_", "sequence", matchers)
        {
        }

        protected override IEnumerable<IMatch> InnerMatches(Context subject, int before, int start)
        {
            var matches = new IMatch[Matchers.Count];

            foreach (var match in InDepth(0, start).Materialize())
            {
                yield return match;
            }

            IEnumerable<IMatch> InDepth(int depth, int next)
            {
                var matcher = Matchers[depth];

                foreach (var match in matcher.Matches(subject, next).Materialize())
                {
                    matches[depth] = match;
                    if (depth == Matchers.Count - 1)
                    {
                        yield return IMatch.Success(this, before, start, match.Next, new List<IMatch>(matches));
                    }
                    else
                    {
                        foreach (var inner in InDepth(depth + 1, match.Next).Materialize())
                        {
                            yield return inner;
                        }
                    }
                }
            }
        }

        protected override bool InnerMatch(Context subject, ref int cursor)
        {
            var start = cursor;
            foreach (var matcher in Matchers)
            {
                if (!matcher.Match(subject, ref cursor))
                {
                    cursor = start;
                    return false;
                }
            }

            return true;
        }

        protected override IMatch InnerMatch(Context subject, int before, int start)
        {
            var matches = new IMatch[Matchers.Count];

            var cursor = start;
            for (var i = 0; i < Matchers.Count; i += 1)
            {
                var match = Matchers[i].Match(subject, cursor);
                if (match == null)
                {
                    return null;
                }
                matches[i] = match;
                cursor = match.Next;
            }

            return IMatch.Success(this, before, start, cursor, matches);
        }

        public override T Accept<T>(IMatcherVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
