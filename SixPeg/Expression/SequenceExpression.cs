using SixPeg.Matchers;
using System.Collections.Generic;
using System.Linq;

namespace SixPeg.Expression
{
    public class SequenceExpression : AnyExpression
    {
        public SequenceExpression(IEnumerable<AnyExpression> expressions)
        {
            Expressions = expressions.ToArray();
        }

        public IReadOnlyList<AnyExpression> Expressions { get; }

        protected override IMatcher MakeMatcher()
        {
            return MatchSequence.From(Expressions.Select(e => e.GetMatcher()));
        }

        protected override void InnerResolve()
        {
            foreach (var expression in Expressions)
            {
                expression.Resolve(Grammar);
            }
        }
    }
}
