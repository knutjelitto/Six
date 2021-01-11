using System.Collections.Generic;

namespace Six.Peg.Expression
{
    public class Terminals : Grules<Rule>
    {
        public Terminals() { }
        public Terminals(IEnumerable<Rule> terminals) : base(terminals) { }
    }
}
