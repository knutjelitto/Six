using SixPeg.Matchers;

namespace SixPeg.Expression
{
    public class AndExpression : AnyExpression
    {
        public AndExpression(AnyExpression expression)
        {
            Expression = expression;
        }

        public AnyExpression Expression { get; }

        protected override IMatcher MakeMatcher()
        {
            return new MatchAnd(Expression.GetMatcher());
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
