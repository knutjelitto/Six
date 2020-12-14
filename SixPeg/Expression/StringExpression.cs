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

        public override IMatcher GetMatcher(bool spaced)
        {
            return new MatchCharacterSequence(spaced, Text);
        }

        public override void Resolve(GrammarExpression grammar)
        {
        }
    }
}
