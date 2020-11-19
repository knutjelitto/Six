namespace SixComp.ParseTree
{
    public class FunctionCallExpression : PostfixExpression
    {
        public FunctionCallExpression(AnyExpression left, ArgumentClause arguments) : base(left)
        {
            Arguments = arguments;
        }

        public ArgumentClause Arguments { get; }

        public static FunctionCallExpression Parse(Parser parser, AnyExpression left)
        {
            var arguments = ArgumentClause.Parse(parser);

            return new FunctionCallExpression(left, arguments);
        }
    }
}
