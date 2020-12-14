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

        public override IMatcher GetMatcher()
        {
            return Expressions.Count switch
            {
                0 => new MatchEpsilon(),
                1 => Expressions[0].GetMatcher(),
                _ => new MatchSequence(Expressions.Select(e => e.GetMatcher())),
            };
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
