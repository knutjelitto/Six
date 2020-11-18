using SixComp.Support;

namespace SixComp.ParseTree
{
    public class Modifier
    {
        public static TokenSet Firsts = new TokenSet(ToKind.KwPublic, ToKind.KwPrivate, ToKind.KwInout);

        private Modifier(Token token)
        {
            Token = token;
        }

        public Token Token { get; }


        public static Modifier Parse(Parser parser)
        {
            var  modifier = parser.Consume(Firsts);

            return new Modifier(modifier);
        }

        public override string ToString()
        {
            return $"{Token}";
        }
    }
}
