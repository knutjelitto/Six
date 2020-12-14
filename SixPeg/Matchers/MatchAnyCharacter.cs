using Six.Support;
using SixPeg.Expression;

namespace SixPeg.Matchers
{
    public class MatchAnyCharacter : AnyMatcher
    {
        public MatchAnyCharacter(WildcardExpression expression)
        {
            Expression = expression;
        }

        public WildcardExpression Expression { get; }

        public override bool Match(string subject, ref int cursor)
        {
            if (cursor < subject.Length)
            {
                cursor += 1;
                return true;
            }
            return false;
        }

        public override void Write(IWriter writer)
        {
            writer.WriteLine($"any-character");
        }
    }
}
