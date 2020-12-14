using SixPeg.Matchers;
using System.Collections.Generic;

namespace SixPeg.Expression
{
    public class ErrorExpression : AnyExpression
    {
        public ErrorExpression(IList<object> arguments)
        {
            Code = 11;
            Arguments = arguments;
        }

        public int Code { get; }
        public IList<object> Arguments { get; }

        public override IMatcher GetMatcher(bool spaced)
        {
            return new MatchError(spaced);
        }

        public override void Resolve(GrammarExpression grammar)
        {
        }
    }
}
