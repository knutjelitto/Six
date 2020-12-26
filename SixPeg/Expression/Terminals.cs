using System.Collections.Generic;

namespace SixPeg.Expression
{
    public class Terminals : Grules<AnyRule>
    {
        public Terminals() { }
        public Terminals(IEnumerable<AnyRule> terminals) : base(terminals) { }
    }
}
