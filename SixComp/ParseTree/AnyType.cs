using System;

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
                default:
                    type = TypeIdentifier.Parse(parser);
                    break;
            }

            switch (parser.Current)
            {
                case ToKind.Quest:
                    parser.ConsumeAny();
                    type = new OptionalType(type);
                    break;
            }

            if (type == null)
            {
                throw new InvalidOperationException("can't parse type");
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

            if (parser.Current == ToKind.MinusGreater)
            {
                return FunctionType.Parse(parser, prefix, funArgs);
            }

            return TupleType.From(prefix, funArgs.Arguments);
        }
    }
}
