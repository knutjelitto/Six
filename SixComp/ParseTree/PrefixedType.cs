namespace SixComp.ParseTree
{
    public class PrefixedType : AnyType
    {
        public PrefixedType(Prefix prefix, AnyType type)
        {
            Prefix = prefix;
            Type = type;
        }

        public Prefix Prefix { get; }
        public AnyType Type { get; }

        public static PrefixedType Parse(Parser parser)
        {
            var prefix = Prefix.Parse(parser);
            var type = AnyType.Parse(parser);

            return new PrefixedType(prefix, type);
        }

        public override string ToString()
        {
            return $"{Prefix}{Type}";
        }
    }
}
