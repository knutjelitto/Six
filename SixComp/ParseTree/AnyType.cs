namespace SixComp.ParseTree
{
    public interface AnyType
    {
        public static AnyType Parse(Parser parser)
        {
            return TypeIdentifier.Parse(parser);
        }
    }
}
