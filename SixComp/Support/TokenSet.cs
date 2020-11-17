using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Support
{
    public struct TokenSet
    {
        private readonly BitArray bits;

        public TokenSet(params ToKind[] kinds)
        {
            bits = new BitArray((int)ToKind._FIN_);
            foreach (var kind in kinds)
            {
                bits.Set((int)kind, true);
            }
        }

        public TokenSet(TokenSet other, params ToKind[] kinds)
        {
            bits = new BitArray(other.bits);
            foreach (var kind in kinds)
            {
                bits.Set((int)kind, true);
            }
        }

        public bool Contains(ToKind kind)
        {
            return bits.Get((int)kind);
        }

        private IEnumerable<ToKind> GetKinds()
        {
            for (var i = 0; i < (int)ToKind._FIN_; i += 1)
            {
                if (bits.Get(i))
                {
                    yield return (ToKind)i;
                }
            }
        }

        public override string ToString()
        {
            var kinds = GetKinds().ToList();

            if (kinds.Count == 1)
            {
                return $"{kinds[0]}";
            }

            var prefix = string.Join("`, `", kinds.Take(kinds.Count - 1));
            var postfx = $" or `{kinds.Last()}`";

            return $"`{prefix}`{postfx}";
        }
    }
}
