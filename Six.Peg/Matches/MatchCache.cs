using Six.Peg.Expression;
using System.Collections.Generic;
using System.Diagnostics;

namespace SixPeg.Matches
{
    public class MatchCache
    {
        private readonly Dictionary<int, IMatch> cache = new Dictionary<int, IMatch>();

        public MatchCache(Symbol name)
        {
            Name = name;
        }

        public Symbol Name { get; }

        [DebuggerStepThrough]
        public bool Already(int cursor, out IMatch result)
        {
            return cache.TryGetValue(cursor, out result);
        }

        [DebuggerStepThrough]
        public void Cache(int cursor, IMatch result)
        {
            cache.Add(cursor, result);
        }

        public void Clear()
        {
            cache.Clear();
        }
    }
}
