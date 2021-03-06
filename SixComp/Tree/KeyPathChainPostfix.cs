﻿using SixComp.Support;

namespace SixComp
{
    public partial class ParseTree
    {
        public class KeyPathChainPostfix : IKeyPathPostfix
        {
            public KeyPathChainPostfix() { }

            public static KeyPathChainPostfix Parse(Parser parser)
            {
                parser.Consume(ToKind.Quest);
                return new KeyPathChainPostfix();
            }

            public override string ToString()
            {
                return $"{ToKind.Quest.GetRep()}";
            }
        }
    }
}