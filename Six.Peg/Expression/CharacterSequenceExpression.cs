using Six.Support;
using SixPeg.Visiting;

namespace Six.Peg.Expression
{
    public class CharacterSequenceExpression : AnyExpression
    {
        public CharacterSequenceExpression(string text)
        {
            Text = text;
        }

        public string Text { get; }

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
