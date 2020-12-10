namespace SixComp
{
    public partial class ParseTree
    {
        public interface ICondition : IExpression
        {
            public static new ICondition? TryParse(Parser parser)
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
}
