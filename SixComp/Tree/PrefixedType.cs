namespace SixComp
{
    public partial class ParseTree
    {
        public class PrefixedType : IType
        {
            public PrefixedType(Prefix prefix, bool inout, IType type)
            {
                Prefix = prefix;
                Inout = inout;
                Type = type;
            }

            public Prefix Prefix { get; }
            public bool Inout { get; }
            public IType Type { get; }

            public static PrefixedType Parse(Parser parser)
            {
                var prefix = Prefix.Parse(parser, onlyAttributes: true);
                var inout = parser.Match(ToKind.KwInout);
                var type = IType.Parse(parser);

                return new PrefixedType(prefix, inout, type);
            }

            public override string ToString()
            {
                return $"{Prefix}{Type}";
            }
        }
    }
}