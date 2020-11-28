using SixComp.Support;

namespace SixComp.ParseTree
{
    public class KeyPathChainPostfix : AnyKeyPathPostfix
    {
        public KeyPathChainPostfix() { }

        public static KeyPathChainPostfix Parse(Parser parser)
        {
            parser.Consume(ToKind.Quest);
            return new KeyPathChainPostfix();
        }

        public override string ToString()
        {
            return $"{ToKind.Quest.GetRep()}";
        }
    }
}
