using SixPeg.Matchers;
using System.Collections.Generic;
using System.Linq;

namespace SixPeg.Expression
{
    public class ChoiceExpression : AnyExpression
    {
        public ChoiceExpression(IList<AnyExpression> expressions)
        {
            Expressions = expressions;
        }

        public IList<AnyExpression> Expressions { get; }

        public override IMatcher GetMatcher(bool spaced)
        {
            return Expressions.Count == 1
                ? Expressions[0].GetMatcher(spaced)
                : new MatchChoice(spaced, Expressions.Select(e => e.GetMatcher(false)));
        }

        public override void Resolve(GrammarExpression grammar)
        {
            foreach (var expression in Expressions)
            {
                expression.Resolve(grammar);
            }
        }
    }
}
