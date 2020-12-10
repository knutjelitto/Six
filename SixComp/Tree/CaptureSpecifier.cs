using SixComp.Common;
using SixComp.Support;

namespace SixComp
{
    public partial class ParseTree
    {
        public class CaptureSpecifier
        {
            private CaptureSpecifier(CaptureKind kind)
            {
                Kind = kind;
            }

            public CaptureKind Kind { get; }

            public static CaptureSpecifier? Parse(Parser parser)
            {
                if (parser.Match(ToKind.KwWeak))
                {
                    return new CaptureSpecifier(CaptureKind.Weak);
                }
                if (parser.Match(ToKind.KwUnowned))
                {
                    if (parser.Match(ToKind.LParent))
                    {
                        if (parser.Match(ToKind.KwSafe))
                        {
                            parser.Consume(ToKind.RParent);
                            return new CaptureSpecifier(CaptureKind.UnownedSafe);
                        }
                        else if (parser.Match(ToKind.KwUnsafe))
                        {
                            parser.Consume(ToKind.RParent);
                            return new CaptureSpecifier(CaptureKind.UnownedUnsafe);
                        }
                        parser.Consume(new TokenSet(ToKind.KwSafe, ToKind.KwUnsafe));
                    }
                    return new CaptureSpecifier(CaptureKind.Unowned);
                }

                return null;
            }
        }
    }
}