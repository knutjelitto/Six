using System.Collections.Generic;
using System.Diagnostics;

namespace Six.Peg.Runtime
{
    public class MatchCache
    {
        private readonly Dictionary<int, Match> cache = new Dictionary<int, Match>();

        public MatchCache()
        {
        }

        [DebuggerStepThrough]
        public bool Already(int cursor, out Match result)
        {
            return cache.TryGetValue(cursor, out result);
        }

        [DebuggerStepThrough]
        public void Cache(int cursor, Match result)
        {
            cache.Add(cursor, result);
        }

        public void Clear()
        {
            cache.Clear();
        }
    }
}
