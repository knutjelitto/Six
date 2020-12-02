using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public abstract class Items<T, TTree> : Items<T>
        where T : IReportable
    {
        public TTree Tree { get; }

        private Items(IScoped outer, TTree tree, List<T> items)
            : base(outer, items)
        {
            Tree = tree;
        }

        public Items(IScoped outer, TTree tree, IEnumerable<T> items)
            : this(outer, tree, items.ToList())
        {
        }

        public Items(IScoped outer, TTree tree)
            : this(outer, tree, new List<T>())
        {
        }
    }
}
