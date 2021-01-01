using SixPeg.Visiting;
using System.Collections.Generic;

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

        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
