namespace SixComp.ParseTree
{
    public interface AnyCondition
    {
        public static AnyCondition Parse(Parser parser)
        {
            switch (parser.Current)
            {
                case ToKind.KwCase:
                case ToKind.KwLet:
                case ToKind.KwVar:
                    return PatternCondition.Parse(parser);
                default:
                    return ExpressionCondition.Parse(parser);
            }
        }
    }
}
