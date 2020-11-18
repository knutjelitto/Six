namespace SixComp.ParseTree
{
    public class TypeAnnotation : AnyType
    {
        private TypeAnnotation(Prefix prefix, AnyType type)
        {
            Prefix = prefix;
            Type = type;
        }

        public Prefix Prefix { get; }
        public AnyType Type { get; }

        public static TypeAnnotation Parse(Parser parser)
        {
            //TODO: prefix 
            parser.Consume(ToKind.Colon);
            var prefix = Prefix.Parse(parser);
            var type = AnyType.Parse(parser);

            return new TypeAnnotation(prefix, type);
        }

        public override string ToString()
        {
            return $": {Prefix}{Type}";
        }
    }
}
