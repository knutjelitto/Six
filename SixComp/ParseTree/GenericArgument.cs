namespace SixComp.ParseTree
{
    public class GenericArgument
    {
        public GenericArgument(IType type)
        {
            Type = type;
        }

        public IType Type { get; }

        public static GenericArgument Parse(Parser parser)
        {
            var type = TypeParser.Parse(parser);

            return new GenericArgument(type);
        }
    }
}
