using Six.Support;
using SixPeg.Matches;
using SixPeg.Runtime;
using SixPeg.Visiting;
using System.Collections.Generic;

namespace SixPeg.Matchers
{
    public class MatchError : AnyMatcher
    {
        public MatchError(IReadOnlyList<object> arguments)
        {
            Arguments = arguments;
        }

        public IReadOnlyList<object> Arguments { get; }
        public override string Marker => "#";

        protected override IEnumerable<IMatch> InnerMatches(Context subject, int before, int start)
        {
            yield break;
        }

        protected override bool InnerMatch(Context subject, ref int cursor)
        {
            var message = string.Join(" ", Arguments);

            new Error(subject).Report(message, furthestCursor);

            throw new System.NotImplementedException(message);
        }

        protected override IMatch InnerMatch(Context subject, int before, int start)
        {
            var message = string.Join(" ", Arguments);

            new Error(subject).Report(message, furthestCursor);

            throw new System.NotImplementedException(message);
        }

        public override T Accept<T>(IMatcherVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
