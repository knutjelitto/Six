using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public abstract class Items<T> : Base, IReadOnlyList<T>
        where T : IReportable
    {
        private List<T> items;

        private Items(IScoped outer, List<T> items)
            : base(outer)
        {
            this.items = items;
        }

        public Items(IScoped outer, IEnumerable<T> items)
            : this(outer, items.ToList())
        {
        }

        public Items(IScoped outer)
            : this(outer, new List<T>())
        {
        }

        protected void Add(T item)
        {
            this.items.Add(item);
        }


        public T this[int index] => items[index];
        public int Count => items.Count;


        public IEnumerator<T> GetEnumerator() => items.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => items.GetEnumerator();
    }
}
