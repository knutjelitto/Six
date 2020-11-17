using SixComp.Support;
using System.Collections;
using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class ItemList<T> : IReadOnlyList<T>, IWritable
    {
        private List<T> items;

        public ItemList(List<T> items)
        {
            this.items = items;
        }

        protected ItemList()
            : this(new List<T>())
        {
            Missing = true;
        }

        public T this[int index] => items[index];
        public int Count => items.Count;

        public bool Missing { get; }

        public IEnumerator<T> GetEnumerator() => items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => items.GetEnumerator();

        public virtual void Write(IWriter writer)
        {
            foreach (var item in this)
            {
                if (item is IWritable writable)
                {
                    writable.Write(writer);
                }
                else
                {
                    writer.WriteLine(item?.ToString() ?? string.Empty);
                }
            }
        }
    }
}
