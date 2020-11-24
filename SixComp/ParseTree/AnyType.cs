using SixComp.Support;
using System;
using System.Linq;

namespace SixComp.ParseTree
{
    public interface AnyType
    {
        public static AnyType Parse(Parser parser)
        {
            var prefix = Prefix.Parse(parser);
            AnyType? type;
            switch (parser.Current)
            {
                case ToKind.LParent:
                    type = TupleOrFunctionType(parser, prefix);
                    break;
                case ToKind.LBracket:
                    type = ArrayOrDictionayType(parser);
                    break;
                case ToKind.KwSELF:
                    type = SELFType.Parse(parser);
                    break;
                case ToKind.KwANY:
                    type = ANIType.Parse(parser);
                    break;
                case ToKind.KwSome:
                    type = SomeType.Parse(parser);
                    break;
                default:
                    type = TypeIdentifier.Parse(parser);
                    break;
            }

            while (true)
            {
                if (parser.Match(ToKind.Quest))
                {
                    type = new OptionalType(type);
                }
                else if (parser.Match(ToKind.Bang))
                {
                    type = new UnwrapType(type);
                }
                else if (parser.Current == ToKind.Dot)
                {
                    type = MetatypeType.Parse(parser, type);
                }
                else
                {
                    var token = parser.CurrentToken;

                    if (token.Length >= 2 && token.IsOperator)
                    {
                        if (token.First == '?')
                        {
                            parser.CarefullyConsume(ToKind.Quest);
                            type = new OptionalType(type);
                            continue;
                        }
                    }

                    break;
                }
            }

            return type;
        }

        private static AnyType ArrayOrDictionayType(Parser parser)
        {
            parser.Consume(ToKind.LBracket);

            var type = AnyType.Parse(parser);

            if (parser.Match(ToKind.Colon))
            {
                var type2 = AnyType.Parse(parser);
                parser.Consume(ToKind.RBracket);
                return DictionaryType.From(type, type2);
            }
            parser.Consume(ToKind.RBracket);
            return ArrayType.From(type);
        }

        private static AnyType TupleOrFunctionType(Parser parser, Prefix prefix)
        {
            var funArgs = FunctionTypeArgumentClause.Parse(parser);

            if (parser.Current == ToKind.Arrow || parser.Current == ToKind.KwThrows)
            {
                return FunctionType.Parse(parser, prefix, funArgs);
            }

            var elements = funArgs.Arguments
                .Select(a => TupleTypeElement.From(a.Label, a.Type))
                .ToList();

            return TupleType.From(prefix, new TupleTypeElementList(elements));
        }
    }
}
