using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class DeclarationList : ItemList<Declaration>
    {
        public DeclarationList(List<Declaration> items) : base(items) { }
    }
}
