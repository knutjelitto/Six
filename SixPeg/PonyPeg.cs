using SixPeg.Runtime;

namespace SixPeg
{
    public class PonyPeg : Pegger.Pony.PonyPegger
    {
        public PonyPeg(Context context)
            : base(context)
        {
        }

#if true
        public override Match Keyword(int start)
        {
            if (!Caches[Cache_Keyword].Already(start, out var result))
            {
                Match space = _space_(start);
                result = null;
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
                        var match = Match.Success(space.Next, next);
                        var more = Match.Success(next);

                        result = Match.Success(start, space, match, more);
                    }
                }
                Caches[Cache_Keyword].Cache(start, result);
            }
            return result;
        }
#endif
    }
}
