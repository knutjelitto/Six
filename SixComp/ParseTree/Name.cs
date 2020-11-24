using SixComp.Support;

namespace SixComp.ParseTree
{
    public class Name
    {
        public static TokenSet Contextual = new TokenSet(
            ToKind.KwGet, ToKind.KwSet, ToKind.KwInit, ToKind.KwOpen, ToKind.KwFor, ToKind.KwStatic, ToKind.KwDynamic,
            ToKind.KwExtension, ToKind.KwPrefix, ToKind.KwPostfix, ToKind.KwANY, ToKind.KwSelf, ToKind.KwSELF, ToKind.KwIs,
            ToKind.KwNone, ToKind.KwSome, ToKind.KwLeft, ToKind.KwRight, ToKind.KwIn, ToKind.KwWhile, ToKind.KwWhere);

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

        public static Name? TryParse(Parser parser, bool withOperators = false)
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
            if (parser.Current == ToKind.Name)
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
