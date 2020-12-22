using SixPeg.Matchers;
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

        protected override IMatcher MakeMatcher()
        {
            Debug.Assert(Expression != null);
            return Expression.GetMatcher();
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
