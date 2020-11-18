namespace SixComp.ParseTree
{
    public class SelfMethodExpression : AnySelfExpression
    {
        private SelfMethodExpression(Token self, Name name)
        {
            Self = self;
            Name = name;
        }

        public Token Self { get; }
        public Name Name { get; }

        public static SelfMethodExpression Parse(Parser parser)
        {
            var self = parser.Consume(ToKind.KwSelf);
            parser.Consume(ToKind.Dot);
            var name = Name.Parse(parser);

            return new SelfMethodExpression(self, name);
        }
    }
}
