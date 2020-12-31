using SixPeg.Matchers;
using SixPeg.Visiting;
using System.Diagnostics;

namespace SixPeg.Expression
{
    public class SpacedExpression : AnyExpression
    {
        public SpacedExpression(Symbol name)
        {
            Name = name;
        }

        public Symbol Name { get; }
        public AnyExpression Expression { get; internal set; }

        protected override AnyMatcher MakeMatcher()
        {
            Debug.Assert(Expression != null);
            return Expression.GetMatcher();
        }

        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
