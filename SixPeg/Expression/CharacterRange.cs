using SixPeg.Matchers;
using System;

namespace SixPeg.Expression
{
    public class CharacterRange : AnyExpression
    {
        public CharacterRange(char min, char max)
        {
            Min = min;
            Max = max;
        }

        public char Min { get; }
        public char Max { get; }

        public override bool Equals(object obj) => obj is CharacterRange other && other.Min == Min && other.Max == Max;
        public override int GetHashCode() => HashCode.Combine(Min, Max);

        public override IMatcher GetMatcher(bool spaced)
        {
            return Min == Max
                ? new MatchSingleCharacter(spaced, Min)
                : (IMatcher)new MatchCharacterRange(spaced, Min, Max);
        }

        public override void Resolve(GrammarExpression grammar)
        {
        }
    }
}
