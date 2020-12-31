using SixPeg.Visiting;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SixPeg.Expression
{
    public abstract class AnyExpressions : AnyExpression, IReadOnlyList<AnyExpression>
    {
        public AnyExpressions(IEnumerable<AnyExpression> expressions)
        {
            Expressions = expressions.ToArray(); ;
        }

        public IReadOnlyList<AnyExpression> Expressions { get; }
        public AnyExpression this[int index] => Expressions[index];
        public int Count => Expressions.Count;
        public IEnumerator<AnyExpression> GetEnumerator() => Expressions.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void AcceptForAll<T>(IExpressionVisitor<T> visitor)
        {
            foreach (var expression in Expressions)
            {
                _ = expression.Accept(visitor);
            }
        }
    }
}
