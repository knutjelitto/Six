using SixComp.Support;

namespace SixComp
{
    public partial class Tree
    {
        public class FunctionResult : AnyType
        {
            public static readonly TokenSet Firsts = new TokenSet(ToKind.Arrow);

            private FunctionResult(AnyType type)
            {
                Type = type;
            }

            public AnyType Type { get; }

            public static FunctionResult Parse(Parser parser)
            {
                parser.Consume(ToKind.Arrow);
                var type = AnyType.Parse(parser);

                return new FunctionResult(type);
            }

            public override string ToString()
            {
                return $" -> {Type}";
            }
        }
    }
}