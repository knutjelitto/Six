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
        public AnyExpression Expression { get; private set; }

        protected override IMatcher MakeMatcher()
        {
            Debug.Assert(Expression != null);
            return Expression.GetMatcher();
        }

        protected override void InnerResolve()
        {
            Expression = Grammar.AddSpaced(this);
        }
    }
}
