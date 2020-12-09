namespace SixComp
{
    public partial class Tree
    {
        public class FunctionTypeArgumentClause
        {
            private FunctionTypeArgumentClause(FunctionTypeArgumentList arguments, Optional variadic)
            {
                Arguments = arguments;
                Variadic = variadic;
            }

            public FunctionTypeArgumentList Arguments { get; }
            public Optional Variadic { get; }

            public static FunctionTypeArgumentClause Parse(Parser parser)
            {
                parser.Consume(ToKind.LParent);

                var arguments = FunctionTypeArgumentList.Parse(parser);
                var variadic = Optional.Parse(parser, ToKind.DotDotDot);

                parser.Consume(ToKind.RParent);

                return new FunctionTypeArgumentClause(arguments, variadic);
            }

            public override string ToString()
            {
                return $"({Arguments}{Variadic})";
            }
        }
    }
}