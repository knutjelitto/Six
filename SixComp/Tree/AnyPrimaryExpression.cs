using System;
using SixComp.Support;

namespace SixComp.Tree
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
                    return NameExpression.Parse(parser);
                case ToKind.LParent:
                    return NestedOrTuple(parser);
                case ToKind.LBracket:
                    return ArrayOrDictionary(parser);
                case ToKind.LBrace:
                    return ClosureExpression.TryParse(parser) ?? throw new InvalidOperationException($"{typeof(AnyPrimaryExpression)}");
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
                case ToKind.CdColumn:
                    return ColumnLiteralExpression.Parse(parser);
                case ToKind.CdFunction:
                    return FunctionLiteralExpression.Parse(parser);
                default:
                    if (BaseName.Contextual.Contains(parser.Current))
                    {
                        return NameExpression.Parse(parser);
                    }
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

        private static AnyPrimaryExpression ArrayOrDictionary(Parser parser)
        {
            var offset = parser.Offset;

            parser.Consume(ToKind.LBracket);
            AnyExpression.TryParse(parser);
            var current = parser.Current;

            parser.Offset = offset;

            if (current == ToKind.Colon)
            {
                return DictionaryLiteral.Parse(parser);
            }
            else
            {
                return ArrayLiteral.Parse(parser);
            }
        }
    }
}
