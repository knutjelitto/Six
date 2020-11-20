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
    }
}
