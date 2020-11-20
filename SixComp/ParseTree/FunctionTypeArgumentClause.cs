namespace SixComp.ParseTree
{
    public class FunctionTypeArgumentClause
    {
        private FunctionTypeArgumentClause(FunctionTypeArgumentList arguments, bool variadic)
        {
            Arguments = arguments;
            Variadic = variadic;
        }

        public FunctionTypeArgumentList Arguments { get; }
        public bool Variadic { get; }

        public static FunctionTypeArgumentClause Parse(Parser parser)
        {
            parser.Consume(ToKind.LParent);

            var arguments = FunctionTypeArgumentList.Parse(parser);
            var variadic = parser.Match(ToKind.DotDotDot);

            parser.Consume(ToKind.RParent);

            return new FunctionTypeArgumentClause(arguments, variadic);
        }
    }
}
