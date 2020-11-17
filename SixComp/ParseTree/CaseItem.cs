namespace SixComp.ParseTree
{
    public class CaseItem : IWritable
    {
        private CaseItem(AnyPattern pattern, WhereClause? where)
        {
            Pattern = pattern;
            Where = where;
        }

        public AnyPattern Pattern { get; }
        public WhereClause? Where { get; }

        public static CaseItem Parse(Parser parser)
        {
            var pattern = AnyPattern.Parse(parser);
            var where = parser.Try(ToKind.KwWhere, WhereClause.Parse);

            return new CaseItem(pattern, where);
        }
    }
}
