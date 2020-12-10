using SixComp.Support;

namespace SixComp
{
    public partial class ParseTree
    {
        public interface IStatement : IWritable
        {
            public static IStatement? TryParse(Parser parser)
            {
                switch (parser.Current)
                {
                    case ToKind.KwIf:
                        return IfStatement.Parse(parser);
                    case ToKind.KwSwitch:
                        return SwitchStatement.Parse(parser);
                    case ToKind.KwGuard:
                        return GuardStatement.Parse(parser);
                    case ToKind.KwFor:
                        return ForInStatement.Parse(parser);
                    case ToKind.KwWhile:
                        return WhileStatement.Parse(parser);
                    case ToKind.KwRepeat:
                        return RepeatStatement.Parse(parser);
                    case ToKind.KwBreak:
                        return BreakStatement.Parse(parser);
                    case ToKind.KwContinue:
                        return ContinueStatement.Parse(parser);
                    case ToKind.KwReturn:
                        return ReturnStatement.Parse(parser);
                    case ToKind.KwDefer:
                        return DeferStatement.Parse(parser);
                    case ToKind.KwYield:
                        return YieldStatement.Parse(parser);
                    case ToKind.KwCase: //TODO: should be protected???
                        break;
                    default:
                        if (BaseName.CanParse(parser) && parser.Next == ToKind.Colon)
                        {
                            return LabeledStatement.Parse(parser);
                        }
                        IStatement? statement = DeclarationStatement.TryParse(parser);
                        if (statement == null)
                        {
                            statement = ExpressionStatement.TryParse(parser);
                        }
                        return statement;
                }

                return null;
            }
        }
    }
}