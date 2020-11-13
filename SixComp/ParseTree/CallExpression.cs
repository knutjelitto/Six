namespace SixComp.ParseTree
{
    public class CallExpression : Expression
    {
        public CallExpression(Expression left, ArgumentList arguments)
        {
            Left = left;
            Arguments = arguments;
        }

        public Expression Left { get; }
        public ArgumentList Arguments { get; }

        public override string ToString()
        {
            var args = string.Join(" ", Arguments);

            return $"(call {Left} {args})";
        }
    }
}
