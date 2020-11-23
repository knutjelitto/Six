namespace SixComp.ParseTree
{
    public class FunctionType : AnyType
    {
        public FunctionType(Prefix prefix, FunctionTypeArgumentClause arguments, bool throws, AnyType type)
        {
            Prefix = prefix;
            Arguments = arguments;
            Throws = throws;
            Type = type;
        }

        public Prefix Prefix { get; }
        public FunctionTypeArgumentClause Arguments { get; }
        public bool Throws { get; }
        public AnyType Type { get; }

        public static FunctionType Parse(Parser parser, Prefix prefix, FunctionTypeArgumentClause arguments)
        {
            var throws = parser.Match(ToKind.KwThrows);
            parser.Consume(ToKind.Arrow);
            var type = AnyType.Parse(parser);

            return new FunctionType(prefix, arguments, throws, type);
        }
    }
}
