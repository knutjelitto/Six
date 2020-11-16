namespace SixComp.ParseTree
{
    public class CallExpression : AnyExpression
    {
        public CallExpression(AnyExpression left, ArgumentClause arguments)
        {
            Left = left;
            Arguments = arguments;
        }

        public AnyExpression Left { get; }
        public ArgumentClause Arguments { get; }

        public static CallExpression Parse(Parser parser, AnyExpression left)
        {
            var arguments = ArgumentClause.Parse(parser);

            return new CallExpression(left, arguments);
        }

        public override string ToString()
        {
            return $"{Left}{Arguments}";
        }
    }
}
