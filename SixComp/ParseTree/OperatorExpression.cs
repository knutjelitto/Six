namespace SixComp.ParseTree
{
    public class OperatorExpression : BaseExpression
    {
        public OperatorExpression(Token @operator)
        {
            Operator = @operator;
        }

        public Token Operator { get; }

        public static OperatorExpression Parse(Parser parser)
        {
            var @operator = parser.ConsumeAny();

            return new OperatorExpression(@operator);
        }
    }
}
