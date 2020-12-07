using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public abstract class Items<TItem, TTree> : Items<TItem>, IWithTree<TTree>
        where TItem : IReportable, IResolveable
        where TTree : notnull
    {
        public TTree Tree { get; }

        private Items(IScoped outer, TTree tree, List<TItem> items)
            : base(outer, items)
        {
            Tree = tree;
        }

        public Items(IScoped outer, TTree tree, IEnumerable<TItem> items)
            : this(outer, tree, items.ToList())
        {
        }

        public Items(IScoped outer, TTree tree)
            : this(outer, tree, new List<TItem>())
        {
        }
    }
}
