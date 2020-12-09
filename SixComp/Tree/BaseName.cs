using SixComp.Support;

namespace SixComp
{
    public partial class Tree
    {
        public class BaseName
        {
            public static TokenSet Contextual = new TokenSet(
                ToKind.KwGet, ToKind.KwSet, ToKind.KwInit, ToKind.KwOpen, ToKind.KwFor, ToKind.KwStatic, ToKind.KwDynamic,
                ToKind.KwExtension, ToKind.KwPrefix, ToKind.KwPostfix, ToKind.KwANY, ToKind.KwSelf, ToKind.KwSELF, ToKind.KwIs,
                ToKind.KwSome, ToKind.KwIn, ToKind.KwWhile, ToKind.KwWhere, ToKind.KwContinue, ToKind.KwBreak, ToKind.KwMutating,
                ToKind.KwAs, ToKind.KwOptional, ToKind.KwDefault, ToKind.KwLazy, ToKind.KwSuper, ToKind.KwClass, ToKind.KwStruct,
                ToKind.KwEnum, ToKind.KwAsync, ToKind.KwPublic, ToKind.KwPrivate, ToKind.KwInternal);

            public BaseName(Token token)
            {
                Token = token;
            }

            public Token Token { get; }

            public static BaseName Parse(Parser parser, bool withOperators = false)
            {
                if (withOperators)
                {
                    if (parser.IsOperator)
                    {
                        return new BaseName(parser.ConsumeAny());
                    }
                }
                if (Contextual.Contains(parser.Current))
                {
                    return new BaseName(parser.ConsumeAny());
                }

                return new BaseName(parser.Consume(ToKind.Name));
            }

            public static bool CanParse(Parser parser, bool withOperators = false, bool withImplicits = true)
            {
                return parser.Current == ToKind.Name && (withImplicits || !parser.IsImplicit)
                    || Contextual.Contains(parser.Current)
                    || withOperators && parser.IsOperator
                    ;
            }

            public static BaseName From(Token anyToken)
            {
                return new BaseName(anyToken);
            }

            public static BaseName? TryParse(Parser parser, bool withOperators = false, bool withImplicits = true)
            {
                if (withOperators)
                {
                    if (parser.IsOperator)
                    {
                        return new BaseName(parser.ConsumeAny());
                    }
                }
                if (Contextual.Contains(parser.Current))
                {
                    return new BaseName(parser.ConsumeAny());
                }
                if (parser.Current == ToKind.Name && (withImplicits || !parser.IsImplicit))
                {
                    return new BaseName(parser.ConsumeAny());
                }

                return null;
            }

            public override string ToString()
            {
                return $"{Token}";
            }
        }
    }
}