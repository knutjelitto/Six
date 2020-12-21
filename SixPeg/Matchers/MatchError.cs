using Six.Support;
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
