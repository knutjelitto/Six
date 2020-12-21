using SixPeg.Matchers;
using System.Collections.Generic;
using System.Linq;

namespace SixPeg.Expression
{
    public class CharacterClassExpression : AnyExpression
    {
        public CharacterClassExpression(IList<CharacterRangeExpression> ranges, bool negated)
        {
            Ranges = ranges;
            Negated = negated;
        }

        public IList<CharacterRangeExpression> Ranges { get; }
        public bool Negated { get; }

        protected override IMatcher MakeMatcher()
        {
            return Ranges.Count == 1
                ? Ranges[0].GetMatcher()
                : new MatchChoice(Ranges.Select(r => r.GetMatcher()));
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
