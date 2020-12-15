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

        protected override void InnerResolve()
        {
            Expression.Resolve(Grammar);
        }
    }
}
