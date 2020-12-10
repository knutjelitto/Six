namespace SixComp
{
    public partial class ParseTree
    {
        public class FunctionType : IType
        {
            public FunctionType(Prefix prefix, FunctionTypeArgumentClause arguments, Optional async, Optional throws, FunctionResult result)
            {
                Prefix = prefix;
                Arguments = arguments;
                Async = async;
                Throws = throws;
                Result = result;
            }

            public Prefix Prefix { get; }
            public FunctionTypeArgumentClause Arguments { get; }
            public Optional Async { get; }
            public Optional Throws { get; }
            public FunctionResult Result { get; }

            public static FunctionType Parse(Parser parser, Prefix prefix, FunctionTypeArgumentClause arguments)
            {
                var async = Optional.Parse(parser, ToKind.KwAsync);
                var throws = Optional.Parse(parser, ToKind.KwThrows);
                var result = FunctionResult.Parse(parser);

                return new FunctionType(prefix, arguments, async, throws, result);
            }

            public override string ToString()
            {
                return $"{Prefix}{Arguments}{Async}{Throws}{Result}";
            }
        }
    }
}