﻿namespace SixComp
{
    public partial class ParseTree
    {
        public class SelfInitExpression : BaseExpression, ISelfExpression
        {
            private SelfInitExpression(Token self, Token init)
            {
                Self = self;
                Init = init;
            }

            public Token Self { get; }
            public Token Init { get; }

            public static SelfInitExpression Parse(Parser parser)
            {
                var self = parser.Consume(ToKind.KwSelf);
                parser.Consume(ToKind.Dot);
                var init = parser.Consume(ToKind.KwInit);

                return new SelfInitExpression(self, init);
            }
        }
    }
}