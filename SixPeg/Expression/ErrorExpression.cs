using SixPeg.Matchers;
using SixPeg.Visiting;
using System.Collections.Generic;
using System.Linq;

namespace SixPeg.Expression
{
    public class ErrorExpression : AnyExpression
    {
        public ErrorExpression(IEnumerable<object> arguments)
        {
            Arguments = arguments.ToArray();
        }

        public IReadOnlyList<object> Arguments { get; }

        protected override AnyMatcher MakeMatcher()
        {
            return new MatchError(Arguments);
        }

        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
