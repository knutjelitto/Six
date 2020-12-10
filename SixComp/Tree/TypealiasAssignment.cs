using SixComp.Support;

namespace SixComp
{
    public partial class ParseTree
    {
        public class TypealiasAssignment : IType
        {
            public static TokenSet Firsts = new TokenSet(ToKind.Assign);

            private TypealiasAssignment(IType type)
            {
                Type = type;
            }

            public IType Type { get; }

            public static TypealiasAssignment Parse(Parser parser)
            {
                parser.Consume(ToKind.Assign);

                var type = IType.Parse(parser);

                return new TypealiasAssignment(type);
            }

            public override string ToString()
            {
                return $" = {Type}";
            }
        }
    }
}