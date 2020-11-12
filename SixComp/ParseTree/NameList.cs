using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class NameList : ItemList<Name>
    {
        public NameList(List<Name> items) : base(items)
        {
        }
    }
}
