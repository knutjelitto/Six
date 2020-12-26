using System.Collections.Generic;

namespace SixPeg.Expression
{
    public class Rules : Grules<AnyRule>
    {
        public Rules() { }
        public Rules(IEnumerable<AnyRule> rules) : base(rules) { }
    }
}
