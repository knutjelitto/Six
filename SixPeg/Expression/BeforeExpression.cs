using SixPeg.Matchers;

namespace SixPeg.Expression
{
    public class BeforeExpression : AnyExpression
    {
        public BeforeExpression(AnyExpression expression)
        {
            Expression = expression;
        }

        public AnyExpression Expression { get; }

        protected override IMatcher MakeMatcher()
        {
            return new MatchBefore(Expression.GetMatcher());
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
