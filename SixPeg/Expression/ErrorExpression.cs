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

        public override IMatcher GetMatcher()
        {
            return new MatchError();
        }

        public override void Resolve(GrammarExpression grammar)
        {
        }
    }
}
