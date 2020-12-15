using SixPeg.Matchers;

namespace SixPeg.Expression
{
    public class StringExpression : AnyExpression
    {
        public StringExpression(string text)
        {
            Text = text;
        }

        public string Text { get; }

        protected override IMatcher MakeMatcher()
        {
            return new MatchCharacterSequence(Text);
        }

        protected override void InnerResolve()
        {
        }
    }
}
