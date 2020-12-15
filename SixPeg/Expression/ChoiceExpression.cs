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

        protected override IMatcher MakeMatcher() => Expressions.Count == 1
                ? Expressions[0].GetMatcher()
                : new MatchChoice(Expressions.Select(e => e.GetMatcher()));

        protected override void InnerResolve()
        {
            foreach (var expression in Expressions)
            {
                _ = expression.Resolve(Grammar);
            }
        }
    }
}
