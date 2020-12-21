using SixPeg.Matchers;

namespace SixPeg.Expression
{
    public class NotExpression : AnyExpression
    {
        public NotExpression(AnyExpression expression)
        {
            Expression = expression;
        }

        public AnyExpression Expression { get; }

        protected override IMatcher MakeMatcher()
        {
            return new MatchNot(Expression.GetMatcher());
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
