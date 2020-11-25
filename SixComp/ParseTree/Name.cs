using SixComp.Support;

namespace SixComp.ParseTree
{
    public class Name
    {
        public static TokenSet Contextual = new TokenSet(
            ToKind.KwGet, ToKind.KwSet, ToKind.KwInit, ToKind.KwOpen, ToKind.KwFor, ToKind.KwStatic, ToKind.KwDynamic,
            ToKind.KwExtension, ToKind.KwPrefix, ToKind.KwPostfix, ToKind.KwANY, ToKind.KwSelf, ToKind.KwSELF, ToKind.KwIs,
            ToKind.KwNone, ToKind.KwSome, ToKind.KwLeft, ToKind.KwRight, ToKind.KwIn, ToKind.KwWhile, ToKind.KwWhere,
            ToKind.KwAs, ToKind.KwOptional, ToKind.KwDefault, ToKind.KwLazy, ToKind.KwSuper, ToKind.KwClass);

        public Name(Token token)
        {
            Token = token;
        }

        public Token Token { get; }

        public static Name Parse(Parser parser, bool withOperators = false)
        {
            if (withOperators)
            {
                if (parser.IsOperator)
                {
                    return new Name(parser.ConsumeAny());
                }
            }
            if (Contextual.Contains(parser.Current))
            {
                return new Name(parser.ConsumeAny());
            }

            return new Name(parser.Consume(ToKind.Name));
        }

        public static bool CanParse(Parser parser, bool withOperators = false, bool withImplicits = true)
        {
            return parser.Current == ToKind.Name && (withImplicits || !parser.IsImplicit)
                || Contextual.Contains(parser.Current)
                || withOperators && parser.IsOperator
                ;
        }

        public static Name? TryParse(Parser parser, bool withOperators = false, bool withImplicits = true)
        {
            if (withOperators)
            {
                if (parser.IsOperator)
                {
                    return new Name(parser.ConsumeAny());
                }
            }
            if (Contextual.Contains(parser.Current))
            {
                return new Name(parser.ConsumeAny());
            }
            if (parser.Current == ToKind.Name && (withImplicits || !parser.IsImplicit))
            {
                return new Name(parser.ConsumeAny());
            }

            return null;
        }

        public override string ToString()
        {
            return $"{Token}";
        }
    }
}
