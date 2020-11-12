namespace SixComp.ParseTree
{
    public class PrefixExpression : Expression
    {
        public PrefixExpression(Token op, Expression operand)
        {
            Op = op;
            Operand = operand;
        }

        public Token Op { get; }
        public Expression Operand { get; }

        public override string ToString()
        {
            return $"({Op.Span}_ {Operand})";
        }
    }
}
