namespace SixComp.ParseTree
{
    public class PostfixExpression : AnyExpression
    {
        public PostfixExpression(AnyExpression operand, Token op)
        {
            Op = op;
            Operand = operand;
        }

        public AnyExpression Operand { get; }
        public Token Op { get; }
    }
}
