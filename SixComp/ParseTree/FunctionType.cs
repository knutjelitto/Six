namespace SixComp.ParseTree
{
    public class FunctionType : AnyType
    {
        public FunctionType(Prefix prefix, FunctionTypeArgumentClause arguments, AnyType type)
        {
            Prefix = prefix;
            Arguments = arguments;
            Type = type;
        }

        public Prefix Prefix { get; }
        public FunctionTypeArgumentClause Arguments { get; }
        public AnyType Type { get; }

        public static FunctionType Parse(Parser parser)
        {
            var prefix = Prefix.Parse(parser);
            var arguments = FunctionTypeArgumentClause.Parse(parser);
            parser.Match(ToKind.KwThrows);
            parser.Consume(ToKind.MinusGreater);
            var type = AnyType.Parse(parser);

            return new FunctionType(prefix, arguments, type);
        }
    }
}
