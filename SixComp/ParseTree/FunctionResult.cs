namespace SixComp.ParseTree
{
    public class FunctionResult : AnyType
    {
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
