using System.Diagnostics;

namespace SixComp
{
    public partial class ParseTree
    {
        public interface ISelfExpression : IPrimaryExpression
        {
            public static ISelfExpression Parse(Parser parser)
            {
                Debug.Assert(parser.Current == ToKind.KwSelf);

                if (parser.Next == ToKind.LBracket)
                {
                    return SelfSubscriptExpression.Parse(parser);
                }

                if (parser.Next == ToKind.Dot)
                {
                    if (parser.NextNext == ToKind.KwInit)
                    {
                        return SelfInitExpression.Parse(parser);
                    }

                    return SelfMethodExpression.Parse(parser);
                }

                return SelfExpression.Parse(parser);
            }

            public class SelfSubscriptExpression : BaseExpression, ISelfExpression
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

            public class SelfMethodExpression : BaseExpression, ISelfExpression
            {
                private SelfMethodExpression(Token self, BaseName name)
                {
                    Self = self;
                    Name = name;
                }

                public Token Self { get; }
                public BaseName Name { get; }

                public static SelfMethodExpression Parse(Parser parser)
                {
                    var self = parser.Consume(ToKind.KwSelf);
                    parser.Consume(ToKind.Dot);
                    var name = BaseName.Parse(parser);

                    return new SelfMethodExpression(self, name);
                }
            }

            public class SelfExpression : BaseExpression, ISelfExpression
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
    }
}