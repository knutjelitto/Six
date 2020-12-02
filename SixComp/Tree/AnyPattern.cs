using SixComp.Support;

namespace SixComp.Tree
{
    public interface AnyPattern : AnySyntaxNode, IWritable
    {
        public static AnyPattern Parse(Parser parser, TokenSet? follows = null)
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
                var offset = parser.Offset;

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
                        var enumCasePattern = EnumCasePattern.Parse(parser);
                        if (follows == null || follows.Contains(parser.Current))
                        {
                            return enumCasePattern;
                        }
                        else
                        {
                            parser.Offset = offset;
                            break;
                        }
                    case ToKind.Name when follows == null || follows.Contains(parser.Next):
                        return IdentifierPattern.Parse(parser);
                    case ToKind.KwIs:
                        return IsPattern.Parse(parser);
                }
                if (BaseName.CanParse(parser) && (follows == null || follows.Contains(parser.Next)))
                {
                    return IdentifierPattern.Parse(parser);
                }
                return ExpressionPattern.Parse(parser);
            }
        }

        public bool NameOnly => false;
    }
}
