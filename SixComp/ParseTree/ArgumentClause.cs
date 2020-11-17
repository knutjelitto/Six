namespace SixComp.ParseTree
{
    public class ArgumentClause
    {
        public ArgumentClause(ArgumentList arguments)
        {
            Arguments = arguments;
        }

        public ArgumentList Arguments { get; }

        public static ArgumentClause Parse(Parser parser)
        {
            parser.Consume(ToKind.LParent);

            var arguments = ArgumentList.Parse(parser);

            parser.Consume(ToKind.RParent);

            return new ArgumentClause(arguments);
        }

        public override string ToString()
        {
            return $"({Arguments})";
        }
    }
}
