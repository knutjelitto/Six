using SixPeg.Expression;
using System.Collections.Generic;
using System.Linq;

namespace SixPeg.Matchers
{
    public abstract class AnyMatchers : AnyMatcher
    {
        public AnyMatchers(IEnumerable<AnyExpression> expressions)
        {
            Expressions = expressions;
        }

        public IEnumerable<AnyExpression> Expressions { get; }
        public IEnumerable<IMatcher> Matchers => Expressions.Select(e => e.GetMatcher());
    }
}
