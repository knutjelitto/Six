using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class TupleType : Items<TupleTypeElement, Tree.TupleType>, IType
    {
        public TupleType(IScoped outer, Tree.TupleType tree)
            : base(outer, tree, Enum(outer, tree))
        {
        }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.TupleType);
        }

        public override string ToString()
        {
            if (Count == 1)
            {
                return this[0].ToString()!;
            }
            return base.ToString()!;
        }

        private static IEnumerable<TupleTypeElement> Enum(IScoped outer, Tree.TupleType tree)
        {
            return tree.Elements.Select(element => new TupleTypeElement(outer, element));
        }
    }
}
