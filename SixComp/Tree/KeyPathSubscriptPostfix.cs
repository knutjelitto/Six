﻿namespace SixComp
{
    public partial class ParseTree
    {
        public class KeyPathSubscriptPostfix : IKeyPathPostfix
        {
            public KeyPathSubscriptPostfix(SubscriptClause subscript)
            {
                Subscript = subscript;
            }

            public SubscriptClause Subscript { get; }

            public static KeyPathSubscriptPostfix Parse(Parser parser)
            {
                var subscript = SubscriptClause.Parse(parser);

                return new KeyPathSubscriptPostfix(subscript);
            }

            public override string ToString()
            {
                return $"{Subscript}";
            }
        }
    }
}