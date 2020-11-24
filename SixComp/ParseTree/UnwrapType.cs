using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp.ParseTree
{
    public class UnwrapType : AnyType
    {
        public UnwrapType(AnyType type)
        {
            Type = type;
        }

        public AnyType Type { get; }

        public override string ToString()
        {
            return $"{Type}!";
        }
    }
}
