using Six.Support;
using SixComp.Support;

namespace SixComp
{
    public partial class ParseTree
    {
        public class CaseItem : IWritable
        {
            private CaseItem(IPattern pattern, WhereClause? where)
            {
                Pattern = pattern;
                Where = where;
            }

            public IPattern Pattern { get; }
            public WhereClause? Where { get; }

            public static CaseItem Parse(Parser parser, TokenSet follows)
            {
                var pattern = IPattern.Parse(parser, new TokenSet(follows, ToKind.KwWhere));
                var where = parser.Try(ToKind.KwWhere, WhereClause.Parse);

                return new CaseItem(pattern, where);
            }

            public override string ToString()
            {
                return $"{Pattern}{Where}";
            }
        }
    }
}