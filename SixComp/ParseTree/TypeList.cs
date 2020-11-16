using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class TypeList : ItemList<AnyType>
    {
        public TypeList(List<AnyType> items) : base(items) { }
        public TypeList() { }

        public override string ToString()
        {
            return string.Join(", ", this);
        }
    }
}
