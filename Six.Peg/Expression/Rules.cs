using System.Collections.Generic;

namespace Six.Peg.Expression
{
    public class Rules : Grules<Rule>
    {
        public Rules() { }
        public Rules(IEnumerable<Rule> rules) : base(rules) { }
    }
}
