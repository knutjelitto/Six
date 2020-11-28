namespace SixComp.ParseTree
{
    public interface AnyCondition : AnyExpression
    {
        public static new AnyCondition? TryParse(Parser parser)
        {
            switch (parser.Current)
            {
                case ToKind.KwCase:
                case ToKind.KwLet:
                case ToKind.KwVar:
                    return PatternCondition.Parse(parser);
                case ToKind.KwIf:
                    return IfCondition.Parse(parser);
                case ToKind.CdAvailable:
                    return AvailableCondition.Parse(parser);
                default:
                    return ExpressionCondition.TryParse(parser);
            }
        }
    }
}
