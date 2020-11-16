namespace SixComp.ParseTree
{
    public class IndexingExpression : AnyExpression
    {
        public IndexingExpression(AnyExpression left, IndexingClause index)
        {
            Left = left;
            Index = index;
        }

        public AnyExpression Left { get; }
        public IndexingClause Index { get; }

        public static IndexingExpression Parse(Parser parser, AnyExpression left)
        {
            var index = IndexingClause.Parse(parser);

            return new IndexingExpression(left, index);
        }
    }
}
