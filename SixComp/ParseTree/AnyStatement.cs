using SixComp.Support;
using System;

namespace SixComp.ParseTree
{
    public interface AnyStatement : IWritable
    {
        public static AnyStatement Parse(Parser parser)
        {
            switch(parser.Current)
            {
                case ToKind.KwIf:
                    return IfStatement.Parse(parser);
                case ToKind.KwBreak:
                    return BreakStatement.Parse(parser);
                case ToKind.KwContinue:
                    return ContinueStatement.Parse(parser);
                case ToKind.KwReturn:
                    return ReturnStatement.Parse(parser);
                case ToKind.KwSwitch:
                    return SwitchStatement.Parse(parser);
                default:
                    return ExpressionStatement.Parse(parser);
            }

            throw new NotImplementedException();
        }
    }
}
