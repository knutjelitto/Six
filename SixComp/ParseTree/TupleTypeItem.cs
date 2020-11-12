using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp.ParseTree
{
    public class TupleTypeItem
    {
        public TupleTypeItem(Name? name, IType type)
        {
            Name = name;
            Type = type;
        }

        public Name? Name { get; }
        public IType Type { get; }
    }
}
