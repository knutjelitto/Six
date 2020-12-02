using System.Collections;
using System.Collections.Generic;

namespace SixComp.Sema
{
    public class NamedList : IReadOnlyList<INamed>
    {
        private List<INamed> named = new List<INamed>();
        public INamed this[int index] => named[index];
        public int Count => named.Count;
        public IEnumerator<INamed> GetEnumerator() => named.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => named.GetEnumerator();
    }
}
