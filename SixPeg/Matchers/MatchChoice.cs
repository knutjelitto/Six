using SixPeg.Matches;
using SixPeg.Visiting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SixPeg.Matchers
{
    public class MatchChoice : BaseMatchers
    {
        private bool? isClassy = null;

        public MatchChoice(IEnumerable<AnyMatcher> matchers)
            : base("|", "choice", matchers)
        {
            Debug.Assert(Matchers.Count >= 2);
        }

        public override bool IsClassy => isClassy ??= Matchers.All(m => m.IsClassy);

        protected override IEnumerable<IMatch> InnerMatches(Context subject, int before, int start)
        {
            foreach (var matcher in Matchers)
            {
                foreach (var match in matcher.Matches(subject, start).Materialize())
                {
                    yield return match;
                }
            }
        }

        protected override bool InnerMatch(Context subject, ref int cursor)
        {
            var start = cursor;
            foreach (var matcher in Matchers)
            {
                if (matcher.Match(subject, ref cursor))
                {
                    return true;
                }
            }
            cursor = start;
            return false;
        }

        protected override IMatch InnerMatch(Context subject, int before, int start)
        {
            foreach (var matcher in Matchers)
            {
                IMatch match;

                if ((match = matcher.Match(subject, start)) != null)
                {
                    return IMatch.Success(this, before, start, match);
                }
            }
            return null;
        }

        public override T Accept<T>(IMatcherVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
