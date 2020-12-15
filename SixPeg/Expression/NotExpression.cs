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

        protected override void InnerResolve()
        {
            Expression.Resolve(Grammar);
        }
    }
}
