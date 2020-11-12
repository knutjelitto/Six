using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp.ParseTree
{
    public class GenericParameter
    {
        public GenericParameter(Name name)
        {
            Name = name;
        }

        public Name Name { get; }
    }
}
