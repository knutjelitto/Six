using System.Diagnostics;

namespace SixComp.ParseTree
{
    public interface AnyPrefixExpression : AnyExpression
    {
        public static new AnyPrefixExpression? TryParse(Parser parser)
        {
            if (parser.Current == ToKind.Amper)
            {
                Debug.Assert(true);
                //return InOutExpression.TryParse(parser);
            }

            var offset = parser.Offset;

            if (parser.IsPrefixOperator())
            {
                var op = parser.ConsumeAny();
                var operand = AnyPostfixExpression.TryParse(parser);
                if (operand != null)
                {
                    if (op.Kind == ToKind.Amper)
                    {
                        return InOutExpression.From(operand);
                    }
                    return new PrefixExpression(op, operand);
                }
                parser.Offset = offset;
                return null;
            }
            return AnyPostfixExpression.TryParse(parser);
        }
    }
}
