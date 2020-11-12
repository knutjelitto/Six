using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp.ParseTree
{
    public class TupleType : ItemList<TupleTypeItem>, IType
    {
        public TupleType(List<TupleTypeItem> items) : base(items) { }
    }
}
