using SixComp.Support;

namespace SixComp.ParseTree
{
    public class Operator
    {
        public static readonly TokenSet Firsts = new TokenSet(
            ToKind.Plus, ToKind.PlusEqual, ToKind.Minus, ToKind.MinusEqual,
            ToKind.Asterisk, ToKind.AsteriskEqual, ToKind.Slash, ToKind.SlashEqual,
            ToKind.EqualEqual, ToKind.BangEqual);

        public Operator(Token token)
        {
        }
    }
}
