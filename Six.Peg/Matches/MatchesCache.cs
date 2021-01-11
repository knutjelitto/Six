using Six.Peg.Expression;
using System.Collections.Generic;
using System.Diagnostics;

namespace SixPeg.Matches
{
    public class MatchesCache
    {
        private readonly Dictionary<int, IReadOnlyList<IMatch>> cache = new Dictionary<int, IReadOnlyList<IMatch>>();

        public MatchesCache(Symbol name)
        {
            Name = name;
        }

        public Symbol Name { get; }

        [DebuggerStepThrough]
        public bool Already(int cursor, out IReadOnlyList<IMatch> result)
        {
            return cache.TryGetValue(cursor, out result);
        }

        [DebuggerStepThrough]
        public void Cache(int cursor, IReadOnlyList<IMatch> result)
        {
            cache.Add(cursor, result);
        }

        public void Clear()
        {
            cache.Clear();
        }
    }
}
