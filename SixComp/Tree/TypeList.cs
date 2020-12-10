using System.Collections.Generic;

namespace SixComp
{
    public partial class ParseTree
    {
        public class TypeList : ItemList<IType>
        {
            public TypeList(List<IType> items) : base(items) { }
            public TypeList() { }

            public override string ToString()
            {
                return string.Join(", ", this);
            }
        }
    }
}