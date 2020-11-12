namespace SixComp.ParseTree
{
    public class PostfixExpression : Expression
    {
        public PostfixExpression(Expression operand, Token op)
        {
            Op = op;
            Operand = operand;
        }

        public Expression Operand { get; }
        public Token Op { get; }
    }
}
