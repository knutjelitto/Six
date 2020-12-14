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

        public override IMatcher GetMatcher(bool spaced)
        {
            return (Quantifier.Min, Quantifier.Max) switch
            {
                (1, 1) => Expression.GetMatcher(spaced),
                (0, 1) => new MatchZeroOrOne(spaced, Expression.GetMatcher(false)),
                (0, null) => new MatchZeroOrMore(spaced, Expression.GetMatcher(false)),
                (1, null) => new MatchOneOrMore(spaced, Expression.GetMatcher(false)),
                _ => throw new System.NotImplementedException(),
            };
        }

        public override void Resolve(GrammarExpression grammar)
        {
            Expression.Resolve(grammar);
        }
    }
}
