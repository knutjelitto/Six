using SixPeg.Matchers;
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

        protected override IMatcher MakeMatcher()
        {
            return MatchSequence.From(Expressions.Select(e => e.GetMatcher()));
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
