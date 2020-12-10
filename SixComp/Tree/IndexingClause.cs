namespace SixComp
{
    public partial class ParseTree
    {
        public class IndexingClause
        {
            public IndexingClause(ArgumentList arguments)
            {
                Arguments = arguments;
            }

            public ArgumentList Arguments { get; }

            public static IndexingClause Parse(Parser parser)
            {
                parser.Consume(ToKind.LBracket);

                var arguments = ArgumentList.Parse(parser);

                parser.Consume(ToKind.RBracket);

                return new IndexingClause(arguments);
            }

            public override string ToString()
            {
                return $"[{Arguments}]";
            }
        }
    }
}