using SixPeg.Matchers;
using SixPeg.Visiting;
using System.Collections.Generic;
using System.Linq;

namespace SixPeg.Expression
{
    public class SequenceExpression : AnyExpressions
    {
        public SequenceExpression(IEnumerable<AnyExpression> expressions)
            : base(expressions)
        {
        }

        protected override AnyMatcher MakeMatcher()
        {
            return MatchSequence.From(Expressions.Select(e => e.GetMatcher()));
        }

        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
