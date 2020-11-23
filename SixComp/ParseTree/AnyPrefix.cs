namespace SixComp.ParseTree
{
    public interface AnyPrefix : AnyExpression
    {
        public static new AnyPrefix? TryParse(Parser parser)
        {
            switch (parser.Current)
            {
                case ToKind.Minus:
                case ToKind.Plus:
                case ToKind.Bang:
                case ToKind.Tilde:
                case ToKind.DotDotLess:
                    return PrefixExpression.Parse(parser);
                case ToKind.Amper:
                    return InOutExpression.Parse(parser);
                default:
                    return AnyPostfix.TryParse(parser);
            }
        }
    }
}
