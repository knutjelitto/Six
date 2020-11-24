namespace SixComp.ParseTree
{
    public interface AnyPostfixExpression : AnyPrefixExpression
    {
        public static new AnyPostfixExpression? TryParse(Parser parser)
        {
            AnyPostfixExpression? left = AnyPrimaryExpression.TryParse(parser);

            if (left == null)
            {
                return null;
            }

            var done = false;
            do
            {
                switch (parser.Current)
                {
                    case ToKind.LBracket:
                        left = SubscriptExpression.Parse(parser, left);
                        break;
                    case ToKind.LParent:
                        left = FunctionCallExpression.Parse(parser, left);
                        break;
                    case ToKind.LBrace when !(left is FunctionCallExpression) && CanAquireBrace(parser):
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

        private static bool CanAquireBrace(Parser parser)
        {
            using (parser.InBacktrack())
            {
                parser.Consume(ToKind.LBrace);

                return !AnyVarDeclaration.CheckWillDit(parser);
            }
        }
    }
}
