using System;
using System.Collections.Generic;
using SixComp.Support;

namespace SixComp.ParseTree
{
    public interface AnyPrimaryExpression : AnyPostfixExpression
    {
        private static TokenSet Firsts = new TokenSet(
            ToKind.Number, ToKind.String, ToKind.Name, ToKind.KwSelf, ToKind.LParent, ToKind.LBracket, ToKind.Dot, ToKind.KwFalse, ToKind.KwTrue,
            ToKind.KwNil, ToKind.Backslash);

        public static new AnyPrimaryExpression? TryParse(Parser parser)
        {
            switch (parser.Current)
            {
                case ToKind.Number:
                    return NumberLiteralExpression.Parse(parser);
                case ToKind.String:
                    return StringLiteralExpression.Parse(parser);
                case ToKind.Name:
                case ToKind.KwSELF:
                    return NameExpression.Parse(parser);
                case ToKind.KwSelf:
                    return AnySelfExpression.Parse(parser);
                case ToKind.LParent:
                    return NestedOrTuple(parser);
                case ToKind.LBracket:
                    return DirayLiteral.Parse(parser);
                case ToKind.LBrace:
                    return ClosureExpression.TryParse(parser) ?? throw new InvalidOperationException();
                case ToKind.Dot:
                    return ImplicitMemberExpression.Parse(parser);
                case ToKind.KwFalse:
                case ToKind.KwTrue:
                    return BoolLiteralExpression.Parse(parser);
                case ToKind.KwNil:
                    return NilLiteralExpression.Parse(parser);
                case ToKind.Backslash:
                    return KeyPathExpression.Parse(parser);
                case ToKind.CdFile:
                    return FileLiteralExpression.Parse(parser);
                case ToKind.CdLine:
                    return LineLiteralExpression.Parse(parser);
                default:
                    return null;
            }
        }

        private static AnyPrimaryExpression NestedOrTuple(Parser parser)
        {
            var token = parser.Consume(ToKind.LParent);

            var elements = TupleElementList.Parse(parser);

            parser.Consume(ToKind.RParent);

            if (elements.Count == 1)
            {
                if (elements[0].Name == null)
                {
                    return NestedExpression.From(elements[0].Value);
                }
                throw new ParserException(token, "there is no such thing as an one element tuple");
            }

            return TupleExpression.From(elements);
        }
    }
}
