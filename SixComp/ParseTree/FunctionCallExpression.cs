namespace SixComp.ParseTree
{
    public class FunctionCallExpression : PostfixExpression
    {
        public FunctionCallExpression(AnyExpression left, ArgumentClause arguments, TrailingClosureList closures) : base(left)
        {
            Arguments = arguments;
            Closures = closures;
        }

        public ArgumentClause Arguments { get; }
        public TrailingClosureList Closures { get; }

        public static FunctionCallExpression Parse(Parser parser, AnyExpression left)
        {
            var arguments = parser.TryList(ArgumentClause.Firsts, ArgumentClause.Parse);
            TrailingClosureList? closures =
                arguments.Missing || TrailingClosure.Try(parser)
                ? TrailingClosureList.Parse(parser)
                : new TrailingClosureList();

            return new FunctionCallExpression(left, arguments, closures);
        }

        public override string ToString()
        {
            return $"{Left}{Arguments}{Closures}";
        }
    }
}
