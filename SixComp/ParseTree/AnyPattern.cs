using SixComp.Support;

namespace SixComp.ParseTree
{
    public interface AnyPattern : IWriteable
    {
        public static AnyPattern Parse(Parser parser)
        {
            switch (parser.Current.Kind)
            {
                case ToKind.Dot:
                    return ExpressionPattern.Parse(parser);
                case ToKind.KwLet:
                    return LetPattern.Parse(parser);
                case ToKind.KwVar:
                    return VarPattern.Parse(parser);
            }
            return IdentifierPattern.Parse(parser);
        }
    }
}
