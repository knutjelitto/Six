using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp.ParseTree
{
    public class FunctionTypeArgumentClause
    {
        public FunctionTypeArgumentClause(FunctionTypeArgumentList arguments, Token? variadic)
        {
            Arguments = arguments;
        }

        public FunctionTypeArgumentList Arguments { get; }

        public static FunctionTypeArgumentClause Parse(Parser parser)
        {
            parser.Consume(ToKind.LParent);

            if (parser.Current != ToKind.RParent)
            {
                var arguments = FunctionTypeArgumentList.Parse(parser);
                var variadic = (Token?)null;
                if (parser.Current == ToKind.DotDotDot)
                {
                    variadic = parser.Consume(ToKind.DotDotDot);
                }

                parser.Consume(ToKind.RParent);

                return new FunctionTypeArgumentClause(arguments, variadic);
            }

            return new FunctionTypeArgumentClause(new FunctionTypeArgumentList(), null);

        }
    }
}
