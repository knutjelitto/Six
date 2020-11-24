namespace SixComp.ParseTree
{
    public class SomeType : AnyType
    {
        public SomeType(AnyType type)
        {
            Type = type;
        }

        public AnyType Type { get; }

        public static SomeType Parse(Parser parser)
        {
            parser.Consume(ToKind.KwSome);
            var type = AnyType.Parse(parser);

            return new SomeType(type);
        }

        public override string ToString()
        {
            return $"some {Type}";
        }
    }
}
