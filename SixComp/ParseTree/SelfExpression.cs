using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp.ParseTree
{
    public class SelfExpression : AnySelfExpression
    {
        private SelfExpression(Token self)
        {
            Self = self;
        }

        public Token Self { get; }

        public static SelfExpression Parse(Parser parser)
        {
            var self = parser.Consume(ToKind.KwSelf);

            return new SelfExpression(self);
        }
    }
}
