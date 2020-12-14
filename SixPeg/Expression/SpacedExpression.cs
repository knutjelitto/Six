using SixPeg.Matchers;

namespace SixPeg.Expression
{
    public class SpacedExpression : AnyExpression
    {
        private IMatcher matcher;

        public SpacedExpression(string text)
        {
            Text = text;
        }

        public string Text { get; }
        public RuleExpression Rule { get; private set; }

        public override IMatcher GetMatcher()
        {
            return matcher ??= Rule.GetMatcher();
        }

        public override void Resolve(GrammarExpression grammar)
        {
            Rule = grammar.AddSpaced(this);
        }
    }
}
