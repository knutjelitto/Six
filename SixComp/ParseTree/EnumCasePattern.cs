namespace SixComp.ParseTree
{
    public class EnumCasePattern : SyntaxNode, AnyPattern
    {
        //
        // enum-case-pattern -> type-name? '.' enum-case-name tuple-pattern?

        public EnumCasePattern(Name? enumName, Name caseName, TuplePattern? tuplePattern)
        {
            EnumName = enumName;
            CaseName = caseName;
            TuplePattern = tuplePattern;
        }

        public Name? EnumName { get; }
        public Name CaseName { get; }
        public TuplePattern? TuplePattern { get; }

        public static AnyPattern Parse(Parser parser)
        {
            var offset = parser.Offset;

            Name? enumName = null;
            if (Name.CanParse(parser))
            {
                enumName = Name.Parse(parser);
            }

            if (parser.Match(ToKind.Dot) && Name.CanParse(parser))
            {
                var caseName = Name.Parse(parser);
                var pattern = parser.Try(ToKind.LParent, TuplePattern.Parse);

                if (!parser.IsOperator)
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
