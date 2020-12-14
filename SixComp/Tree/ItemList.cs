using Six.Support;
using System.Collections;
using System.Collections.Generic;

namespace SixComp
{
    public partial class ParseTree
    {
        public class ItemList<T> : ISyntaxNode, IReadOnlyList<T>, IWritable
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

            public bool Missing { get; private set; }

            public NodeData? Data { get; set; }

            protected void ClearToMissing()
            {
                items.Clear();
                Missing = true;
            }

            protected void Backdoor(T item)
            {
                items.Add(item);
                Missing = false;
            }

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
}