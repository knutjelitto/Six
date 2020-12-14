using SixPeg.Matchers;
using System.Collections.Generic;
using System.Linq;

namespace SixPeg.Expression
{
    public class ClassExpression : AnyExpression
    {
        public ClassExpression(IList<CharacterRange> ranges, bool negated)
        {
            Ranges = ranges;
            Negated = negated;
        }

        public IList<CharacterRange> Ranges { get; }
        public bool Negated { get; }

        public override IMatcher GetMatcher(bool spaced)
        {
            return Ranges.Count == 1
                ? Ranges[0].GetMatcher(spaced)
                : new MatchChoice(spaced, Ranges.Select(r => r.GetMatcher(false)));
        }

        public override void Resolve(GrammarExpression grammar)
        {
        }
    }
}
