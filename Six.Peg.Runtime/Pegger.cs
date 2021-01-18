using Six.Support;
using System;

namespace Six.Peg.Runtime
{
    public class Pegger
    {
        protected readonly MatchCache[] Caches;
        protected readonly Context Context;
        protected readonly int ContextLength;
        protected readonly string ContextText;
        public Pegger(Context context, int cachesCount)
        {
            Context = context;

            ContextLength = Context.Text.Length;
            ContextText = Context.Text;

            Caches = new MatchCache[cachesCount];
            for (var index = 0; index < cachesCount; index += 1)
            {
                Caches[index] = new MatchCache();
            }
        }


        public Match Not_(int start, Match match)
        {
            if (match == null)
            {
                return Match.Success("!", start);
            }
            return null;
        }

        public Match And_(int start, Match match)
        {
            if (match != null)
            {
                return Match.Success("&", start );
            }
            return null;
        }

        public Match Terminal_(Match space, string text, Func<int, Match> more)
        {
            var start = space.Next;
            if (start + text.Length <= ContextLength && MemoryExtensions.Equals(text.AsSpan(), ContextText.AsSpan(start, text.Length), StringComparison.Ordinal))
            {
                var next = start + text.Length;
                if (more == null || more(next) == null)
                {
                    var match = Match.Success($"'{text.Escape()}'", start, next);
                    match.Before = space.Start;
                    return match;
                }
            }
            return null;
        }

        public Match CharacterAny_(int start)
        {
            if (start < ContextLength)
            {
                return Match.Success($"==.", start, start + 1);
            }
            return null;
        }

        public Match CharacterExact_(int start, char exact)
        {
            if (start < ContextLength && ContextText[start] == exact)
            {
                return Match.Success($"=={exact.AsCharLiteral()}", start, start + 1);
            }
            return null;
        }

        public Match CharacterRange_(int start, char min, char max)
        {
            if (start < ContextLength && min <= ContextText[start] && ContextText[start] <= max)
            {
                return Match.Success($"{min.AsCharLiteral()}..{max.AsCharLiteral()}", start, start + 1);
            }
            return null;
        }


        public Match CharacterSequence_(int start, string text)
        {
            if (start + text.Length <= ContextLength && MemoryExtensions.Equals(text.AsSpan(), ContextText.AsSpan(start, text.Length), StringComparison.Ordinal))
            {
                return Match.Success($"=={text.AsStringLiteral()}", start, start + text.Length);
            }
            return null;
        }

        public Match CharacterSet_(int start, string set)
        {
            if (start < ContextLength && set.Contains(ContextText[start]))
            {
                return Match.Success($"==[{set.Escape()}]", start, start + 1);
            }
            return null;
        }
    }
}
