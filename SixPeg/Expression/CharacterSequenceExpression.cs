using Six.Support;
using SixPeg.Matchers;
using SixPeg.Visiting;

namespace SixPeg.Expression
{
    public class CharacterSequenceExpression : AnyExpression
    {
        public CharacterSequenceExpression(string text)
        {
            Text = text;
        }

        public string Text { get; }

        protected override AnyMatcher MakeMatcher()
        {
            return Text.Length == 1 
                ? (AnyMatcher)new MatchCharacterExact(Text[0])
                : new MatchCharacterSequence(Text);
        }

        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            return Text.Escape();
        }
    }
}
