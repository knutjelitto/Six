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

        public override IMatcher GetMatcher()
        {
            return Ranges.Count == 1
                ? Ranges[0].GetMatcher()
                : new MatchCharacterClass(Ranges.Select(r => r.GetMatcher()));
        }

        public override void Resolve(GrammarExpression grammar)
        {
        }
    }
}
