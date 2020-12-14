using SixPeg.Matchers;
using System.Diagnostics;

namespace SixPeg.Expression
{
    public class SpacedExpression : AnyExpression
    {
        public SpacedExpression(string text)
        {
            Text = text;
        }

        public string Text { get; }
        public RuleExpression Rule { get; private set; }

        public override IMatcher GetMatcher(bool spaced)
        {
            Debug.Assert(Rule != null);
            return Rule.GetMatcher(spaced);
        }

        public override void Resolve(GrammarExpression grammar)
        {
            Rule = grammar.AddSpaced(this);
        }
    }
}
