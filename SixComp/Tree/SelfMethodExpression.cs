namespace SixComp.Tree
{
    public class SelfMethodExpression : BaseExpression, AnySelfExpression
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
}
