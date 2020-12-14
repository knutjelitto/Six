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

        public override IMatcher GetMatcher(bool spaced)
        {
            return new MatchNot(spaced, Expression.GetMatcher(false));
        }

        public override void Resolve(GrammarExpression grammar)
        {
            Expression.Resolve(grammar);
        }
    }
}
