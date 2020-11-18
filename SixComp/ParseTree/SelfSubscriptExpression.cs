using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp.ParseTree
{
    public class SelfSubscriptExpression : AnySelfExpression
    {
        private SelfSubscriptExpression(Token self, SubscriptClause subscript)
        {
            Self = self;
            Subscript = subscript;
        }

        public Token Self { get; }
        public SubscriptClause Subscript { get; }

        public static SelfSubscriptExpression Parse(Parser parser)
        {
            var self = parser.Consume(ToKind.KwSelf);
            var subscript = SubscriptClause.Parse(parser);

            return new SelfSubscriptExpression(self, subscript);
        }
    }
}
