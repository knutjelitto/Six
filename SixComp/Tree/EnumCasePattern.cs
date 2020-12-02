namespace SixComp.Tree
{
    public class EnumCasePattern : SyntaxNode, AnyPattern
    {
        //
        // enum-case-pattern -> type-name? '.' enum-case-name tuple-pattern?

        public EnumCasePattern(BaseName? enumName, BaseName caseName, TuplePattern? tuplePattern)
        {
            EnumName = enumName;
            CaseName = caseName;
            TuplePattern = tuplePattern;
        }

        public BaseName? EnumName { get; }
        public BaseName CaseName { get; }
        public TuplePattern? TuplePattern { get; }

        public static AnyPattern Parse(Parser parser)
        {
            var offset = parser.Offset;

            BaseName? enumName = null;
            if (BaseName.CanParse(parser))
            {
                enumName = BaseName.Parse(parser);
            }

            if (parser.Match(ToKind.Dot) && BaseName.CanParse(parser))
            {
                var caseName = BaseName.Parse(parser);
                var pattern = parser.Try(ToKind.LParent, TuplePattern.Parse);

                if (parser.Current != ToKind.Dot)
                {
                    return new EnumCasePattern(enumName, caseName, pattern);
                }
            }

            parser.Offset = offset;
            return ExpressionPattern.Parse(parser);
        }

        public override string ToString()
        {
            return $"{EnumName}.{CaseName}{TuplePattern}";
        }
    }
}
