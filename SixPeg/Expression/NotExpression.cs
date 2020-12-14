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

        public override IMatcher GetMatcher()
        {
            return new MatchNot(Expression.GetMatcher());
        }

        public override void Resolve(GrammarExpression grammar)
        {
            Expression.Resolve(grammar);
        }
    }
}
