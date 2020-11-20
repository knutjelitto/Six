namespace SixComp.ParseTree
{
    public class KeyPathSelfPostfix : AnyKeyPathPostfix
    {
        public KeyPathSelfPostfix() { }

        public static KeyPathSelfPostfix Parse(Parser parser)
        {
            parser.Consume(ToKind.KwSelf);
            return new KeyPathSelfPostfix();
        }
    }
}
