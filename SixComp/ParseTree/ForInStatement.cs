namespace SixComp.ParseTree
{
    public class ForInStatement : AnyStatement
    {
        public ForInStatement(AnyPattern pattern, AnyExpression expression, WhereClause? where, CodeBlock block)
        {
            Pattern = pattern;
            Expression = expression;
            Where = where;
            Block = block;
        }

        public AnyPattern Pattern { get; }
        public AnyExpression Expression { get; }
        public WhereClause? Where { get; }
        public CodeBlock Block { get; }

        public static ForInStatement Parse(Parser parser)
        {
            parser.Consume(ToKind.KwFor);
            parser.Match(ToKind.KwCase);
            var pattern = AnyPattern.Parse(parser);
            parser.Consume(ToKind.KwIn);
            var expression = AnyExpression.Parse(parser);
            var where = parser.Try(WhereClause.Firsts, WhereClause.Parse);
            var block = CodeBlock.Parse(parser);

            return new ForInStatement(pattern, expression, where, block);
        }
    }
}
