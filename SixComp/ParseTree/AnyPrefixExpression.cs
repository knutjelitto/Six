namespace SixComp.ParseTree
{
    public interface AnyPrefixExpression : AnyExpression
    {
        public static new AnyPrefixExpression? TryParse(Parser parser)
        {
            if (parser.Current == ToKind.Amper)
            {
                return InOutExpression.TryParse(parser);
            }

            var offset = parser.Offset;

            if (parser.IsPrefixOperator())
            {
                var op = parser.ConsumeAny();
                var operand = AnyPostfixExpression.TryParse(parser);
                if (operand != null)
                {
                    return new PrefixExpression(op, operand);
                }
                parser.Offset = offset;
                return null;
            }
            return AnyPostfixExpression.TryParse(parser);
        }
    }
}
