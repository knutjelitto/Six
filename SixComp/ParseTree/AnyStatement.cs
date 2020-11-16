using SixComp.Support;
using System;

namespace SixComp.ParseTree
{
    public interface AnyStatement : IWriteable
    {
        public static AnyStatement Parse(Parser parser)
        {
            switch(parser.Current.Kind)
            {
                case ToKind.KwIf:
                    return IfStatement.Parse(parser);
                case ToKind.KwBreak:
                    return BreakStatement.Parse(parser);
                case ToKind.KwContinue:
                    return ContinueStatement.Parse(parser);
                case ToKind.KwReturn:
                    return ReturnStatement.Parse(parser);
                default:
                    return ExpressionStatement.Parse(parser);
            }

            throw new NotImplementedException();
        }
    }
}
