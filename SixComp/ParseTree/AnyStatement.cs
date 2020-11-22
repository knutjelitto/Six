using SixComp.Support;

namespace SixComp.ParseTree
{
    public interface AnyStatement : IWritable
    {
        public static AnyStatement? TryParse(Parser parser)
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
                case ToKind.KwGuard:
                    return GuardStatement.Parse(parser);
                case ToKind.KwFor:
                    return ForInStatement.Parse(parser);
                default:
                    var statement = (AnyStatement?)DeclarationStatement.TryParse(parser);
                    if (statement == null)
                    {
                        statement = ExpressionStatement.TryParse(parser);
                    }
                    return statement;
            }

            //return null;
        }
    }
}
