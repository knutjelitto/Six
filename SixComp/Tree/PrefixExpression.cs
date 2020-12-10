namespace SixComp
{
    public partial class ParseTree
    {
        public class PrefixExpression : BaseExpression, IPrefixExpression
        {
            public PrefixExpression(Token op, IExpression operand)
            {
                Op = op;
                Operator = BaseName.From(Op);
                Operand = operand;
            }

            public Token Op { get; }
            public BaseName Operator { get; }
            public IExpression Operand { get; }

            public override IExpression? LastExpression
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
}