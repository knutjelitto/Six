using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp.ParseTree
{
    public class EnumCasePattern : AnyPattern
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

        public static EnumCasePattern Parse(Parser parser)
        {
            var enumName = (Name?)null;

            if (parser.Current == ToKind.Name)
            {
                enumName = Name.Parse(parser);
            }

            parser.Consume(ToKind.Dot);
            var caseName = Name.Parse(parser);
            var pattern = parser.Try(ToKind.LParent, TuplePattern.Parse);

            return new EnumCasePattern(enumName, caseName, pattern);
        }

        public override string ToString()
        {
            return $"{EnumName}.{CaseName}{TuplePattern}";
        }
    }
}
