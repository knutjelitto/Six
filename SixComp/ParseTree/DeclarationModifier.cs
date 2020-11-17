using SixComp.Support;
using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp.ParseTree
{
    public class DeclarationModifier
    {
        public static TokenSet Firsts = new TokenSet(ToKind.KwPublic);

        private DeclarationModifier(Token modifier)
        {
            Modifier = modifier;
        }

        public Token Modifier { get; }

        public static DeclarationModifier Parse(Parser parser)
        {
            parser.Consume
        }
    }
}
