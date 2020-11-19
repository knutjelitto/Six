namespace SixComp.ParseTree
{
    public class TypealiasAssignment : AnyType
    {
        private TypealiasAssignment(AnyType type)
        {
            Type = type;
        }

        public AnyType Type { get; }

        public static TypealiasAssignment Parse(Parser parser)
        {
            parser.Consume(ToKind.Equal);

            var type = AnyType.Parse(parser);

            return new TypealiasAssignment(type);
        }
    }
}
