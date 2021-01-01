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

        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
