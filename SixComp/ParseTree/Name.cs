using SixComp.Support;

namespace SixComp.ParseTree
{
    public class Name
    {
        private static TokenSet Contextual = new TokenSet(
            ToKind.KwGet, ToKind.KwSet, ToKind.KwInit, ToKind.KwOpen, ToKind.KwFor, ToKind.KwStatic, ToKind.KwDynamic,
            ToKind.KwExtension, ToKind.KwPrefix, ToKind.KwPostfix);

        public Name(Token token)
        {
            Token = token;
        }

        public Token Token { get; }

        public static Name Parse(Parser parser, bool withOperators = false)
        {
            if (withOperators)
            {
                if (parser.CurrentToken.IsOperator)
                {
                    return new Name(parser.ConsumeAny());
                }
            }
            if (parser.Current == ToKind.KwSELF || Contextual.Contains(parser.Current))
            {
                return new Name(parser.ConsumeAny());
            }

            return new Name(parser.Consume(ToKind.Name));
        }

        public override string ToString()
        {
            return $"{Token}";
        }
    }
}
