using SixComp.Support;
using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp.ParseTree
{
    public class Operator
    {
        public static readonly TokenSet Firsts = new TokenSet(
            ToKind.Plus, ToKind.PlusEqual, ToKind.Minus, ToKind.MinusEqual, ToKind.Asterisk, ToKind.AsteriskEqual, ToKind.Slash, ToKind.SlashEqual);

        public Operator(Token token)
        {
        }
    }
}
