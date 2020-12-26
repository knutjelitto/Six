using SixPeg.Matches;
using System.Collections.Generic;

namespace SixPeg.Matchers
{
    public class MatchOneOrMore : BaseMatcher
    {
        public MatchOneOrMore(IMatcher matcher)
            : base("+", "one-or-more", matcher)
        {
        }

        protected override IEnumerable<IMatch> InnerMatches(Context subject, int before, int start)
        {
            var matches = new List<IMatch>();

            foreach (var match in Inner(0, start).Materialize())
            {
                yield return match;
            }

            IEnumerable<IMatch> Inner(int depth, int next)
            {
                foreach (var outer in Matcher.Matches(subject, next).Materialize())
                {
                    if (matches.Count == depth)
                    {
                        matches.Add(outer);
                    }
                    else
                    {
                        matches[depth] = outer;
                    }

                    foreach (var inner in Inner(depth + 1, outer.Next).Materialize())
                    {
                        yield return inner;
                    }

                    yield return IMatch.Success(this, before, start, outer.Next, matches.GetRange(0, depth + 1));
                }
            }
        }

        protected override bool InnerMatch(Context subject, ref int cursor)
        {
            var matched = false;
            while (Matcher.Match(subject, ref cursor))
            {
                matched = true;
            }

            return matched;
        }
    }
}
