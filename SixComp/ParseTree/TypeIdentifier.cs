using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class TypeIdentifier : ItemList<TypeName>, IType
    {
        public TypeIdentifier(List<TypeName> names) : base(names) { }

        public override string ToString()
        {
            return string.Join(".", this);
        }
    }
}
