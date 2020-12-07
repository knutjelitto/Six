using SixComp.Support;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public abstract class Items<TItem> : Base, IReadOnlyList<TItem>
        where TItem : IReportable, IResolveable
    {
        private List<TItem> items;

        private Items(IScoped outer, List<TItem> items)
            : base(outer)
        {
            this.items = items;
        }

        public Items(IScoped outer, IEnumerable<TItem> items)
            : this(outer, items.ToList())
        {
        }

        public Items(IScoped outer)
            : this(outer, new List<TItem>())
        {
        }

        public void ResolveItems(IWriter writer)
        {
            foreach (var resolveable in this)
            {
                resolveable.Resolve(writer);
            }
        }

        protected void Add(TItem item)
        {
            this.items.Add(item);
        }

        public TItem this[int index] => items[index];
        public int Count => items.Count;
        public IEnumerator<TItem> GetEnumerator() => items.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => items.GetEnumerator();
    }
}
