using SixPeg.Visiting;
using System.Collections.Generic;

namespace Six.Peg.Expression
{
    public class ChoiceExpression : AnyExpressions
    {
        public ChoiceExpression(IList<AnyExpression> expressions)
            : base(expressions)
        {
        }

        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
