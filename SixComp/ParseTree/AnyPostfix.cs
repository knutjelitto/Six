using System;

namespace SixComp.ParseTree
{
    public interface AnyPostfix : AnyExpression
    {
        public static AnyExpression Parse(Parser parser, AnyExpression left)
        {
            var done = false;
            do
            {
                switch (parser.Current)
                {
                    case ToKind.Bang:
                        left = ForcedValueExpression.Parse(parser, left);
                        break;
                    case ToKind.Quest when parser.Adjacent:
                        left = OptionalChainingExpression.Parse(parser, left);
                        break;
                    case ToKind.LBracket:
                        left = SubscriptExpression.Parse(parser, left);
                        break;
                    case ToKind.LParent:
                        left = FunctionCallExpression.Parse(parser, left);
                        break;
                    case ToKind.LBrace:
                        if (TrailingClosure.Try(parser))
                        {
                            left = FunctionCallExpression.Parse(parser, left);
                        }
                        else
                        {
                            done = true;
                        }
                        break;
                    case ToKind.Dot:
                        {
                            switch (parser.Next)
                            {
                                case ToKind.KwSelf:
                                    left = PostfixSelfExpression.Parse(parser, left);
                                    break;
                                case ToKind.KwInit:
                                    left = InitializerExpression.Parse(parser, left);
                                    break;
                                default:
                                    left = ExplicitMemberExpression.Parse(parser, left);
                                    break;
                            }
                            break;
                        }
                    default:
                        done = true;
                        break;
                }
            }
            while (!done);

            return left;
        }
    }
}
