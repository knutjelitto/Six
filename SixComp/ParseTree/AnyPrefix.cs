namespace SixComp.ParseTree
{
    public interface AnyPrefix : AnyExpression
    {
        public static new AnyPrefix? TryParse(Parser parser)
        {
            if (parser.Current == ToKind.Amper)
            {
                return InOutExpression.TryParse(parser);
            }

            var offset = parser.Offset;

            if (parser.IsPrefix())
            {
                var op = parser.ConsumeAny();
                var operand = AnyPostfix.TryParse(parser);
                if (operand != null)
                {
                    return new PrefixExpression(op, operand);
                }
                parser.Offset = offset;
                return null;
            }
            return AnyPostfix.TryParse(parser);
        }
    }
}
