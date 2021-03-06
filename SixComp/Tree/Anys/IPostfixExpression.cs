﻿using System.Diagnostics;

namespace SixComp
{
    public partial class ParseTree
    {
        public interface IPostfixExpression : IPrefixExpression
        {
            public static new IPostfixExpression? TryParse(Parser parser)
            {
                IPostfixExpression? left = IPrimaryExpression.TryParse(parser);

                if (left == null)
                {
                    return null;
                }

                var done = false;
                do
                {
                    if (parser.CurrentToken.Text == "?")
                    {
                        Debug.Assert(true);
                    }
                    switch (parser.Current)
                    {
                        case ToKind.LBracket:
                            left = SubscriptExpression.Parse(parser, left);
                            break;
                        case ToKind.LParent when !parser.CurrentToken.NewlineBefore:
                            left = FunctionCallExpression.Parse(parser, left);
                            break;
                        case ToKind.LBrace when CanAquireBraceForFunction(parser, left):
                            left = FunctionCallExpression.Parse(parser, left);
                            break;
                        case ToKind.Dot:
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
                        default:
                            if (parser.IsPostfixOperator())
                            {
                                left = PostfixOpExpression.Parse(parser, left);
                            }
                            else
                            {
                                done = true;
                            }
                            break;
                    }
                }
                while (!done);

                return left;
            }

            private static bool CanAquireBraceForFunction(Parser parser, IExpression left)
            {
                if (left is FunctionCallExpression || left is TupleExpression)
                {
                    return false;
                }

                using (parser.InBacktrack())
                {
                    parser.Consume(ToKind.LBrace);

                    return !IVarDeclaration.CheckWillSetDitSet(parser);
                }
            }
        }
    }
}