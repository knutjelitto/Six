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

        public override IMatcher GetMatcher(bool spaced)
        {
            return MatchSequence.From(spaced, Expressions.Select(e => e.GetMatcher(false)));
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
