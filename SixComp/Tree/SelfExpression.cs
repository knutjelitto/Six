namespace SixComp
{
    public partial class Tree
    {
        public class SelfExpression : BaseExpression, AnySelfExpression
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