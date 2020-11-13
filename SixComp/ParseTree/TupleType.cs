using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class TupleType : ItemList<TupleTypeItem>, IType
    {
        public TupleType(List<TupleTypeItem> items) : base(items) { }
        public TupleType() { }

        public override string ToString()
        {
            return $"({ string.Join(", ", this) })";
        }
    }
}
