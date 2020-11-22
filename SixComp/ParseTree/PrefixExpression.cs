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

        public static PrefixExpression Parse(Parser parser)
        {
            var op = parser.ConsumeAny();
            var operand = AnyPostfix.TryParse(parser) ?? throw new InvalidOperationException();

            return new PrefixExpression(op, operand);
        }

        public override string ToString()
        {
            return $"({Op.Span}_ {Operand})";
        }
    }
}
