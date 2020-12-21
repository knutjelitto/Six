using SixPeg.Matchers;
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

        protected override IMatcher MakeMatcher()
        {
            return new MatchError(Arguments);
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
