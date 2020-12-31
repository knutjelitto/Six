using SixPeg.Matchers;
using SixPeg.Visiting;
using System;

namespace SixPeg.Expression
{
    public class CharacterRangeExpression : AnyExpression
    {
        public CharacterRangeExpression(char min, char max)
        {
            Min = min;
            Max = max;
        }

        public char Min { get; }
        public char Max { get; }

        public override bool Equals(object obj) => obj is CharacterRangeExpression other && other.Min == Min && other.Max == Max;
        public override int GetHashCode() => HashCode.Combine(Min, Max);

        protected override AnyMatcher MakeMatcher()
        {
            return Min == Max
                ? (AnyMatcher)new MatchCharacterExact(Min)
                : new MatchCharacterRange(Min, Max);
        }

        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
