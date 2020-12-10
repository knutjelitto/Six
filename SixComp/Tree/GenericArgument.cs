namespace SixComp
{
    public partial class ParseTree
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
                var type = IType.Parse(parser);

                return new GenericArgument(type);
            }

            public override string ToString()
            {
                return $"{Type}";
            }
        }
    }
}