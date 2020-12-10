using SixComp.Support;

namespace SixComp
{
    public partial class ParseTree
    {
        public class FunctionResult : IType
        {
            public static readonly TokenSet Firsts = new TokenSet(ToKind.Arrow);

            private FunctionResult(IType type)
            {
                Type = type;
            }

            public IType Type { get; }

            public static FunctionResult Parse(Parser parser)
            {
                parser.Consume(ToKind.Arrow);
                var type = IType.Parse(parser);

                return new FunctionResult(type);
            }

            public override string ToString()
            {
                return $" -> {Type}";
            }
        }
    }
}