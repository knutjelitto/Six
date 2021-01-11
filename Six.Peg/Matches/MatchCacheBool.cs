using Six.Peg.Expression;
using System.Collections.Generic;
using System.Diagnostics;

namespace SixPeg.Matches
{
    public class MatchCacheBool
    {
        private readonly Dictionary<int, (bool, int)> cache = new Dictionary<int, (bool, int)>();

        public MatchCacheBool(Symbol name)
        {
            Name = name;
        }

        public Symbol Name { get; }

        [DebuggerStepThrough]
        public bool Already(int cursor, out (bool result, int cursor) result)
        {
            return cache.TryGetValue(cursor, out result);
        }

        [DebuggerStepThrough]
        public void Cache(int cursor, (bool, int) result)
        {
            cache.Add(cursor, result);
        }

        public void Clear()
        {
            cache.Clear();
        }
    }
}
