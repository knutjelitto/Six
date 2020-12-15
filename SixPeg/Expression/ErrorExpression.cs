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

        protected override IMatcher MakeMatcher()
        {
            return new MatchError();
        }

        protected override void InnerResolve()
        {
        }
    }
}
