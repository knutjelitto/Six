namespace SixComp.ParseTree
{
    public class PrefixExpression : AnyExpression
    {
        public PrefixExpression(Token op, AnyExpression operand)
        {
            Op = op;
            Operand = operand;
        }

        public Token Op { get; }
        public AnyExpression Operand { get; }

        public override string ToString()
        {
            return $"({Op.Span}_ {Operand})";
        }
    }
}
