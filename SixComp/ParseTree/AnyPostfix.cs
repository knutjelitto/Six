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
                    case ToKind.Quest:
                        left = HandlePossibleTernary(parser, left);
                        if (left is ConditionalExpression)
                        {
                            return left;
                        }
                        break;
                    case ToKind.LBracket:
                        left = SubscriptExpression.Parse(parser, left);
                        break;
                    case ToKind.LParent:
                        left = FunctionCallExpression.Parse(parser, left);
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

        private static AnyExpression HandlePossibleTernary(Parser parser, AnyExpression left)
        {
            parser.Consume(ToKind.Quest);

            var offset = parser.Offset;

            var expr1 = AnyExpression.Parse(parser, Precedence.Ternary);

            if (parser.Match(ToKind.Colon))
            {
                var expr2 = AnyExpression.Parse(parser, Precedence.Ternary + 1);

                return new ConditionalExpression(left, expr1, expr2);
            }

            parser.Offset = offset;

            return OptionalChainingExpression.From(left);
        }
    }
}
