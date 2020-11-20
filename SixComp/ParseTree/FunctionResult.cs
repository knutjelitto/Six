using SixComp.Support;

namespace SixComp.ParseTree
{
    public class FunctionResult : AnyType
    {
        public static readonly TokenSet Firsts = new TokenSet(ToKind.MinusGreater);

        private FunctionResult(AnyType type)
        {
            Type = type;
        }

        public AnyType Type { get; }

        public static FunctionResult Parse(Parser parser)
        {
            parser.Consume(ToKind.MinusGreater);
            var type = AnyType.Parse(parser);

            return new FunctionResult(type);
        }
    }
}
