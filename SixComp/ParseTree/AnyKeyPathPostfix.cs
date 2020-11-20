using SixComp.Support;
using System;

namespace SixComp.ParseTree
{
    public interface AnyKeyPathPostfix
    {
        public static readonly TokenSet Firsts = new TokenSet(ToKind.KwSelf, ToKind.Quest, ToKind.Bang, ToKind.LBracket);

        public static AnyKeyPathPostfix Parse(Parser parser)
        {
            switch (parser.Current)
            {
                case ToKind.KwSelf:
                    return KeyPathSelfPostfix.Parse(parser);
                case ToKind.Quest:
                    return KeyPathChainPostfix.Parse(parser);
                case ToKind.Bang:
                    return KeyPathForcePostfix.Parse(parser);
                case ToKind.LBracket:
                    return KeyPathSubscriptPostfix.Parse(parser);
            }

            parser.Consume(Firsts);

            throw new NotSupportedException();
        }
    }
}
