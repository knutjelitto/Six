using System;
using SixComp.Support;

namespace SixComp
{
    public partial class ParseTree
    {
        public interface IPrimaryExpression : IPostfixExpression
        {
            private static TokenSet Firsts = new TokenSet(
                ToKind.Number, ToKind.String, ToKind.Name, ToKind.KwSelf, ToKind.LParent, ToKind.LBracket, ToKind.Dot, ToKind.False, ToKind.True,
                ToKind.KwNil, ToKind.Backslash);

            public static new IPrimaryExpression? TryParse(Parser parser)
            {
                switch (parser.Current)
                {
                    case ToKind.Number:
                        return ILiteralExpression.NumberLiteralExpression.Parse(parser);
                    case ToKind.String:
                        return ILiteralExpression.StringLiteralExpression.Parse(parser);
                    case ToKind.Name:
                        return NameExpression.Parse(parser);
                    case ToKind.LParent:
                        return NestedOrTuple(parser);
                    case ToKind.LBracket:
                        return ArrayOrDictionary(parser);
                    case ToKind.LBrace:
                        return ClosureExpression.TryParse(parser) ?? throw new InvalidOperationException($"{typeof(IPrimaryExpression)}");
                    case ToKind.Dot:
                        return ImplicitMemberExpression.Parse(parser);
                    case ToKind.False:
                    case ToKind.True:
                        return ILiteralExpression.BoolLiteralExpression.Parse(parser);
                    case ToKind.KwNil:
                        return ILiteralExpression.NilLiteralExpression.Parse(parser);
                    case ToKind.Backslash:
                        return KeyPathExpression.Parse(parser);
                    case ToKind.CdFile:
                        return ILiteralExpression.FileLiteralExpression.Parse(parser);
                    case ToKind.CdLine:
                        return ILiteralExpression.LineLiteralExpression.Parse(parser);
                    case ToKind.CdColumn:
                        return ILiteralExpression.ColumnLiteralExpression.Parse(parser);
                    case ToKind.CdFunction:
                        return ILiteralExpression.FunctionLiteralExpression.Parse(parser);
                    default:
                        if (BaseName.Contextual.Contains(parser.Current))
                        {
                            return NameExpression.Parse(parser);
                        }
                        return null;
                }
            }

            private static IPrimaryExpression NestedOrTuple(Parser parser)
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

            private static IPrimaryExpression ArrayOrDictionary(Parser parser)
            {
                var offset = parser.Offset;

                parser.Consume(ToKind.LBracket);
                IExpression.TryParse(parser);
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
}