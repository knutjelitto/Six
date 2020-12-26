using System.Collections.Generic;
using System.Linq;

namespace SixPeg.Expression
{
    public class GrammarPart
    {
        public GrammarPart(Symbol name, IEnumerable<IGrules> grules)
        {
            Name = name;
            Grules = grules;
        }

        public GrammarPart(Rules rules)
            : this(null, Enumerable.Repeat(rules, 1))
        {
        }

        public Symbol Name { get; }
        public IEnumerable<IGrules> Grules { get; }
    }
}
