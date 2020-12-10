namespace SixComp
{
    public partial class ParseTree
    {
        public class SubscriptClause
        {
            private SubscriptClause(ArgumentList arguments)
            {
                Arguments = arguments;
            }

            public ArgumentList Arguments { get; }

            public static SubscriptClause Parse(Parser parser)
            {
                parser.Consume(ToKind.LBracket);

                var arguments = ArgumentList.Parse(parser);

                parser.Consume(ToKind.RBracket);

                return new SubscriptClause(arguments);
            }

            public override string ToString()
            {
                return $"[{Arguments}]";
            }
        }
    }
}