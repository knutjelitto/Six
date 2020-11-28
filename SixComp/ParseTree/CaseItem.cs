﻿using SixComp.Support;

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

        public static CaseItem Parse(Parser parser, TokenSet follows)
        {
            var pattern = AnyPattern.Parse(parser, new TokenSet(follows, ToKind.KwWhere));
            var where = parser.Try(ToKind.KwWhere, WhereClause.Parse);

            return new CaseItem(pattern, where);
        }

        public override string ToString()
        {
            return $"{Pattern}{Where}";
        }
    }
}
