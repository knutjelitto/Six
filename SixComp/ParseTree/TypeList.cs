using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class TypeList : ItemList<IType>
    {
        public TypeList(List<IType> items) : base(items)
        {
        }
    }
}
