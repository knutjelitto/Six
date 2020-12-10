using System.Diagnostics;
using System.Linq;

namespace SixComp
{
    public partial class ParseTree
    {
        public interface IType
        {
            public static IType Parse(Parser parser)
            {
                var prefix = Prefix.Parse(parser, onlyAttributes: true);
                IType? type;
                switch (parser.Current)
                {
                    case ToKind.LParent:
                        type = TupleOrFunctionType(parser, prefix);
                        break;
                    case ToKind.LBracket:
                        type = ArrayOrDictionayType(parser);
                        break;
                    case ToKind.KwSome:
                        type = SomeType.Parse(parser);
                        break;
                    default:
                        if (parser.Current == ToKind.Dot)
                        {
                            Debug.Assert(true);
                        }
                        type = TypeIdentifier.Parse(parser);
                        break;
                }

                while (true)
                {
                    if (parser.IsPostfixOperator())
                    {
                        if (parser.Match(ToKind.Quest))
                        {
                            type = new OptionalType(type);
                        }
                        else if (parser.Match(ToKind.Bang))
                        {
                            type = new UnwrapType(type);
                        }
                        else
                        {
                            var token = parser.CurrentToken;

                            if (token.Length >= 2 && token.IsOperator)
                            {
                                if (token.First == '?')
                                {
                                    parser.ConsumeCarefully(ToKind.Quest);
                                    type = new OptionalType(type);
                                    continue;
                                }
                            }

                            break;
                        }
                    }
                    else if (parser.IsInfixOperator() && parser.Current == ToKind.Amper)
                    {
                        return ProtocolCompositionType.Parse(parser, type);
                    }
                    else
                    {
                        break;
                    }
                }

                return type;
            }

            private static IType ArrayOrDictionayType(Parser parser)
            {
                parser.Consume(ToKind.LBracket);

                var type = IType.Parse(parser);

                if (parser.Match(ToKind.Colon))
                {
                    var type2 = IType.Parse(parser);
                    parser.Consume(ToKind.RBracket);
                    return DictionaryType.From(type, type2);
                }
                parser.Consume(ToKind.RBracket);
                return ArrayType.From(type);
            }

            private static IType TupleOrFunctionType(Parser parser, Prefix prefix)
            {
                var funArgs = FunctionTypeArgumentClause.Parse(parser);

                if (parser.Current == ToKind.Arrow || parser.Current == ToKind.KwThrows || parser.Current == ToKind.KwAsync)
                {
                    return FunctionType.Parse(parser, prefix, funArgs);
                }

                var elements = funArgs.Arguments
                    .Select(a => TupleTypeElement.From(a.Extern, a.Type))
                    .ToList();

                return TupleType.From(prefix, new TupleTypeElementList(elements));
            }
        }
    }
}