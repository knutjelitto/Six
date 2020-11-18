using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp.ParseTree
{
    public class SameTypeRequirement : AnyRequirement
    {
        private SameTypeRequirement(TypeIdentifier typeIdentifier, AnyType type)
        {
            TypeIdentifier = typeIdentifier;
            Type = type;
        }

        public TypeIdentifier TypeIdentifier { get; }
        public AnyType Type { get; }

        public static SameTypeRequirement From(TypeIdentifier typeIdentifier, AnyType type)
        {
            return new SameTypeRequirement(typeIdentifier, type);
        }
    }
}
