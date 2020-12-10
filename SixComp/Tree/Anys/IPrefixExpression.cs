using System.Diagnostics;

namespace SixComp
{
    public partial class ParseTree
    {
        public interface IPrefixExpression : IExpression
        {
            public static new IPrefixExpression? TryParse(Parser parser)
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
                    var operand = IPostfixExpression.TryParse(parser);
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
                return IPostfixExpression.TryParse(parser);
            }
        }
    }
}