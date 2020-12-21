using Six.Support;
using SixPeg.Matchers;

namespace SixPeg.Expression
{
    public class CharacterSequenceExpression : AnyExpression
    {
        public CharacterSequenceExpression(string text)
        {
            Text = text;
        }

        public string Text { get; }

        protected override IMatcher MakeMatcher()
        {
            return Text.Length == 1 
                ? new MatchCharacterExact(Text[0]) 
                : (IMatcher)new MatchCharacterSequence(Text);
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            return Text.Escape();
        }
    }
}
