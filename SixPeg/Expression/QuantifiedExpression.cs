using SixPeg.Matchers;

namespace SixPeg.Expression
{
    public class QuantifiedExpression : AnyExpression
    {
        public QuantifiedExpression(AnyExpression expression, Quantifier quantifier)
        {
            Expression = expression;
            Quantifier = quantifier;
        }

        public AnyExpression Expression { get; }
        public Quantifier Quantifier { get; }

        protected override IMatcher MakeMatcher()
        {
            return (Quantifier.Min, Quantifier.Max) switch
            {
                (1, 1) => Expression.GetMatcher(),
                (0, 1) => new MatchZeroOrOne(Expression.GetMatcher()),
                (0, null) => new MatchZeroOrMore(Expression.GetMatcher()),
                (1, null) => new MatchOneOrMore(Expression.GetMatcher()),
                _ => throw new System.NotImplementedException(),
            };
        }

        protected override void InnerResolve()
        {
            Expression.Resolve(Grammar);
        }
    }
}
