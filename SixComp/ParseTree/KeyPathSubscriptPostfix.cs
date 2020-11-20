namespace SixComp.ParseTree
{
    public class KeyPathSubscriptPostfix : AnyKeyPathPostfix
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
    }
}
