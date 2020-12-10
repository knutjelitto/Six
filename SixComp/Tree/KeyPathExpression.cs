using SixComp.Support;


namespace SixComp
{
    public partial class ParseTree
    {
        public class KeyPathExpression : BaseExpression, IPrimaryExpression
        {
            public static readonly TokenSet Firsts = new TokenSet(ToKind.Backslash);

            public KeyPathExpression(IType? type, KeyPathComponentList components)
            {
                Type = type;
                Components = components;
            }

            public IType? Type { get; }
            public KeyPathComponentList Components { get; }

            public static KeyPathExpression Parse(Parser parser)
            {
                parser.Consume(Firsts);

                var type = parser.Current == ToKind.Dot
                    ? null
                    : IType.Parse(parser);

                var components = KeyPathComponentList.Parse(parser);

                return new KeyPathExpression(type, components);
            }

            public override string ToString()
            {
                return $"\\.{Type}{Components}";
            }
        }
    }
}