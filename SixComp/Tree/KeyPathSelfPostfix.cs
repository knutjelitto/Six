﻿using SixComp.Support;

namespace SixComp
{
    public partial class Tree
    {
        public class KeyPathSelfPostfix : AnyKeyPathPostfix
        {
            public KeyPathSelfPostfix()
            {
            }
            public static KeyPathSelfPostfix Parse(Parser parser)
            {
                parser.Consume(ToKind.KwSelf);
                return new KeyPathSelfPostfix();
            }
            public override string ToString()
            {
                return $"{ToKind.KwSelf.GetRep()}";
            }
        }
    }
}
