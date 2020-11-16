namespace SixComp.ParseTree
{
    public class GenericArgument
    {
        public GenericArgument(AnyType type)
        {
            Type = type;
        }

        public AnyType Type { get; }

        public static GenericArgument Parse(Parser parser)
        {
            var type = AnyType.Parse(parser);

            return new GenericArgument(type);
        }
    }
}
