using SixComp.Support;


namespace SixComp.ParseTree
{
    public class KeyPathExpression : BaseExpression, AnyPrimaryExpression
    {
        public static readonly TokenSet Firsts = new TokenSet(ToKind.Backslash);

        public KeyPathExpression(AnyType? type, KeyPathComponentList components)
        {
            Type = type;
            Components = components;
        }

        public AnyType? Type { get; }
        public KeyPathComponentList Components { get; }

        public static KeyPathExpression Parse(Parser parser)
        {
            parser.Consume(Firsts);

            var type = parser.Current == ToKind.Dot
                ? null
                : AnyType.Parse(parser);

            var components = KeyPathComponentList.Parse(parser);

            return new KeyPathExpression(type, components);
        }
    }
}
