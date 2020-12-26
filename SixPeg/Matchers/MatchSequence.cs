using SixPeg.Matches;
using System.Collections.Generic;
using System.Linq;

namespace SixPeg.Matchers
{
    public class MatchSequence : BaseMatchers
    {
        private MatchSequence(IEnumerable<IMatcher> matchers)
            : base("_", "sequence", matchers)
        {
        }

        public static IMatcher From(IEnumerable<IMatcher> ms)
        {
            var matchers = ms.ToList();

            if (matchers.Count == 0)
            {
                return new MatchEpsilon();
            }
            if (matchers.Count == 1)
            {
                return matchers[0];
            }

            var first = matchers.First();

            if (first is MatchSpace space)
            {
                var rest = From(matchers.Skip(1));
                //Debug.Assert(space.Matcher != null);
                rest.Space = space;
                return rest;
            }

            return new MatchSequence(matchers);
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
    }
}
