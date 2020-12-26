using SixPeg.Matchers;
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

        protected override IMatcher MakeMatcher()
        {
            return Expressions.Count == 1
                ? Expressions[0].GetMatcher()
                : new MatchChoice(Expressions.Select(e => e.GetMatcher()));
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
