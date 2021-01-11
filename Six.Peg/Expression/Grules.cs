using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Six.Peg.Expression
{
    public class Grules<T> : IReadOnlyList<T>, IGrules
    {
        private readonly List<T> grules;

        public Grules(IEnumerable<T> items)
        {
            grules = items.ToList();
        }

        public Grules()
            : this(Enumerable.Empty<T>())
        {
        }

        public T this[int index] => grules[index];
        public int Count => grules.Count;
        public IEnumerator<T> GetEnumerator() => grules.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
