namespace SixComp.Tree
{
    public class PrefixExpression : BaseExpression, AnyPrefixExpression
    {
        public PrefixExpression(Token op, AnyExpression operand)
        {
            Op = op;
            Operator = BaseName.From(Op);
            Operand = operand;
        }

        public Token Op { get; }
        public BaseName Operator { get; }
        public AnyExpression Operand { get; }

        public override AnyExpression? LastExpression
        {
            get
            {
                var last = Operand.LastExpression;
                return last;
            }
        }

        public override string ToString()
        {
            return $"{Op}{Operand}";
        }
    }
}
