namespace SixComp
{
    public partial class ParseTree
    {
        public class FunctionCallExpression : PostfixExpression
        {
            private FunctionCallExpression(IExpression left, Token op, ArgumentClause arguments, TrailingClosureList closures) : base(left, op)
            {
                Arguments = arguments;
                Closures = closures;
            }

            public ArgumentClause Arguments { get; }
            public TrailingClosureList Closures { get; }

            public bool IsDrained => Arguments.Missing && Closures.Count == 0;

            public static FunctionCallExpression Parse(Parser parser, IExpression left)
            {
                var op = parser.CurrentToken;

                var arguments = parser.TryList(ArgumentClause.Firsts, ArgumentClause.Parse);
                var closures = parser.TryList(TrailingClosureList.Firsts, TrailingClosureList.Parse);

                return new FunctionCallExpression(left, op, arguments, closures);
            }

            public override string ToString()
            {
                return $"{Left}{Arguments}{Closures}";
            }
        }
    }
}