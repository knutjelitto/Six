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

        public override IMatcher GetMatcher()
        {
            return Expressions.Count == 1 
                ? Expressions[0].GetMatcher()
                : new MatchChoice(Expressions.Select(e => e.GetMatcher()));
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
