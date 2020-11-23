using System;

namespace SixComp.ParseTree
{
    public class PrefixExpression : BaseExpression, AnyPrefix
    {
        public PrefixExpression(Token op, AnyExpression operand)
        {
            Op = op;
            Operand = operand;
        }

        public Token Op { get; }
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
            return $"({Op}_ {Operand})";
        }
    }
}
