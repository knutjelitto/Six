using SixComp.Support;

namespace SixComp.Tree
{
    public enum CaptureSpectifierKind
    {
        Weak,
        Unowned,
        UnownedSafe,
        UnownedUnsafe,
    }

    public class CaptureSpecifier
    {
        private CaptureSpecifier(CaptureSpectifierKind kind)
        {
            Kind = kind;
        }

        public CaptureSpectifierKind Kind { get; }

        public static CaptureSpecifier? Parse(Parser parser)
        {
                if (parser.Match(ToKind.KwWeak))
                {
                    return new CaptureSpecifier(CaptureSpectifierKind.Weak);
                }
                if (parser.Match(ToKind.KwUnowned))
                {
                    if (parser.Match(ToKind.LParent))
                    {
                        if (parser.Match(ToKind.KwSafe))
                        {
                            parser.Consume(ToKind.RParent);
                            return new CaptureSpecifier(CaptureSpectifierKind.UnownedSafe);
                        }
                        else if (parser.Match(ToKind.KwUnsafe))
                        {
                            parser.Consume(ToKind.RParent);
                            return new CaptureSpecifier(CaptureSpectifierKind.UnownedUnsafe);
                        }
                        parser.Consume(new TokenSet(ToKind.KwSafe, ToKind.KwUnsafe));
                    }
                    return new CaptureSpecifier(CaptureSpectifierKind.Unowned);
                }

                return null;
        }
    }
}
