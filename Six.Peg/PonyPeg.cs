using Six.Peg.Runtime;

namespace SixPeg
{
#if true
    public class PonyPeg
    {
        public PonyPeg(Context context)
        {
        }

        public Match Module(int start)
        {
            return null;
        }
    }
#else
    public class PonyPeg : Pegger.Pony.PonyPegger
    {
        public PonyPeg(Context context)
            : base(context)
        {
        }

        public override Match Identifier(int start)
        {
            if (!Caches[Cache_Identifier].Already(start, out var match))
            {
                Match space = _space_(start);
                var next = space.Next;
                if (next < Context.Length && (char.IsLetter(Context.Text[next]) || Context.Text[next] == '_'))
                {
                    next += 1;
                    while (next < Context.Length && (char.IsLetterOrDigit(Context.Text[next]) || Context.Text[next] == '_'))
                    {
                        next += 1;
                    }
                    while (next < Context.Length && Context.Text[next] == '\'')
                    {
                        next += 1;
                    }
                    var mayKeyword = Context.Text[space.Next..next];
                    if (_keywords.Contains(mayKeyword))
                    {
                        match = null;
                    }
                    else
                    {
                        match = Match.Success(start, space, Match.Success(space.Next, next));
                    }
                }
                Caches[Cache_Identifier].Cache(start, match);
            }
            return match;
        }
    }
#endif
}
