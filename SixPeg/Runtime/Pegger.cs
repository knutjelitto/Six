using System;

namespace SixPeg.Runtime
{
    public class Pegger
    {
        protected readonly MatchCache[] Caches;
        public Pegger(Context context, int cachesCount)
        {
            Context = context;

            Caches = new MatchCache[cachesCount];
            for (var index = 0; index < cachesCount; index += 1)
            {
                Caches[index] = new MatchCache();
            }
        }

        public Context Context { get; }


        public Match Not_(int start, Match match)
        {
            if (match == null)
            {
                return Match.Success(start);
            }
            return null;
        }

        protected Match CharacterAny_(int start)
        {
            if (start < Context.Length)
            {
                return Match.Success(start, start + 1);
            }
            return null;
        }

        protected Match CharacterExact_(int start, char exact)
        {
            if (start < Context.Length && Context.Text[start] == exact)
            {
                return Match.Success(start, start + 1);
            }
            return null;
        }

        protected Match CharacterRange_(int start, char min, char max)
        {
            if (start < Context.Length && min <= Context.Text[start] && Context.Text[start] <= max)
            {
                return Match.Success(start, start + 1);
            }
            return null;
        }


        protected Match CharacterSequence_(int start, string text)
        {
            if (start + text.Length <= Context.Length && MemoryExtensions.Equals(text.AsSpan(), Context.Text.AsSpan(start, text.Length), StringComparison.Ordinal))
            {
                return Match.Success(start, start + text.Length);
            }
            return null;
        }

        protected Match CharacterSet_(int start, string set)
        {
            if (start < Context.Length && set.Contains(Context.Text[start]))
            {
                return Match.Success(start, start + 1);
            }
            return null;
        }
    }
}
