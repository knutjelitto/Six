namespace SixComp.Tree
{
    public class PrefixedType : AnyType
    {
        public PrefixedType(Prefix prefix, bool inout, AnyType type)
        {
            Prefix = prefix;
            Inout = inout;
            Type = type;
        }

        public Prefix Prefix { get; }
        public bool Inout { get; }
        public AnyType Type { get; }

        public static PrefixedType Parse(Parser parser)
        {
            var prefix = Prefix.PreParse(parser, onlyAttributes: true);
            var inout = parser.Match(ToKind.KwInout);
            var type = AnyType.Parse(parser);

            return new PrefixedType(prefix, inout, type);
        }

        public override string ToString()
        {
            return $"{Prefix}{Type}";
        }
    }
}
