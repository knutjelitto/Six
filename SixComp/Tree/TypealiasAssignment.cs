using SixComp.Support;

namespace SixComp
{
    public partial class Tree
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

            public override string ToString()
            {
                return $" = {Type}";
            }
        }
    }
}