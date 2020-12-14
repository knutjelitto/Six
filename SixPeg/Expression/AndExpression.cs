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

        public override IMatcher GetMatcher(bool spaced)
        {
            return new MatchAnd(spaced, Expression.GetMatcher(false));
        }

        public override void Resolve(GrammarExpression grammar)
        {
            Expression.Resolve(grammar);
        }
    }
}
