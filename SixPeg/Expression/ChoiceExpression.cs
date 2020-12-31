using SixPeg.Matchers;
using SixPeg.Visiting;
using System.Collections.Generic;
using System.Linq;

namespace SixPeg.Expression
{
    public class ChoiceExpression : AnyExpressions
    {
        public ChoiceExpression(IList<AnyExpression> expressions)
            : base(expressions)
        {
        }

        protected override AnyMatcher MakeMatcher()
        {
            return Expressions.Count == 1
                ? Expressions[0].GetMatcher()
                : new MatchChoice(Expressions.Select(e => e.GetMatcher()));
        }

        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
