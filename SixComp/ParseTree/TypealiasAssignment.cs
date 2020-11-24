using SixComp.Support;

namespace SixComp.ParseTree
{
    public class TypealiasAssignment : AnyType
    {
        public static TokenSet Firsts = new TokenSet(ToKind.Assign);

        private TypealiasAssignment(AnyType type)
        {
            Type = type;
        }

        public AnyType Type { get; }

        public static TypealiasAssignment Parse(Parser parser)
        {
            parser.Consume(ToKind.Assign);

            var type = AnyType.Parse(parser);

            return new TypealiasAssignment(type);
        }
    }
}
