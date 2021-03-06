﻿using System.Collections.Generic;
using System.Linq;

namespace Six.Peg.Expression
{
    public class Attributes
    {
        public Attributes(IEnumerable<Symbol> symbols)
        {
            Symbols = symbols.ToArray();
        }

        public Attributes()
            : this(Enumerable.Empty<Symbol>())
        {
        }

        public IReadOnlyList<Symbol> Symbols { get; }
    }
}
