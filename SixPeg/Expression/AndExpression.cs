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

        public override IMatcher GetMatcher()
        {
            return new MatchAnd(Expression);
        }

        public override void Resolve(GrammarExpression grammar)
        {
            Expression.Resolve(grammar);
        }
    }
}
