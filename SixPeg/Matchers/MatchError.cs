using Six.Support;
using SixPeg.Matches;
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

            new Error(subject).Report(message, furthestSuccess);

            throw new System.NotImplementedException(message);
        }

        public override void Write(IWriter writer)
        {
            writer.WriteLine($"{SpacePrefix}#error(...)");
        }
    }
}
