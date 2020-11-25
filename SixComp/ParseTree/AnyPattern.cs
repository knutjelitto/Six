using SixComp.Support;

namespace SixComp.ParseTree
{
    public interface AnyPattern : AnySyntaxNode, IWritable
    {
        public static AnyPattern Parse(Parser parser)
        {
            var pattern = Pattern(parser);

            if (parser.Current == ToKind.Quest)
            {
                pattern = OptionalPattern.Parse(parser, pattern);
            }
            if (parser.Current == ToKind.KwAs)
            {
                pattern = AsPattern.Parse(parser, pattern);
            }

            return pattern;

            AnyPattern Pattern(Parser parser)
            {
                switch (parser.Current)
                {
                    case ToKind.LParent:
                        return TuplePattern.Parse(parser);
                    case ToKind.KwLet:
                        return LetPattern.Parse(parser);
                    case ToKind.KwVar:
                        return VarPattern.Parse(parser);
                    case ToKind.Dot:
                    case ToKind.Name when parser.Next == ToKind.Dot:
                        return EnumCasePattern.Parse(parser);
                    case ToKind.Name:
                        return IdentifierPattern.Parse(parser);
                    case ToKind.KwIs:
                        return IsPattern.Parse(parser);
                    default:
                        if (Name.CanParse(parser))
                        {
                            return IdentifierPattern.Parse(parser);
                        }
                        return ExpressionPattern.Parse(parser);
                }
            }
        }

        public bool NameOnly => false;
    }
}
