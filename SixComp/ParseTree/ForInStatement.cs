using SixComp.Support;
using System;

namespace SixComp.ParseTree
{
    public class ForInStatement : SyntaxNode, IWritable, AnyStatement
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
            var type = parser.Try(TypeAnnotation.Firsts, TypeAnnotation.Parse);
            parser.Consume(ToKind.KwIn);
            var expression = AnyExpression.TryParse(parser) ?? throw new InvalidOperationException($"{typeof(ForInStatement)}");
            var where = parser.Try(WhereClause.Firsts, WhereClause.Parse);
            CodeBlock block;
            var last = where == null
                ? expression.LastExpression
                : where.Expression.LastExpression;
            if (parser.Current != ToKind.LBrace && last is FunctionCallExpression call && call.Closures.BlockOnly)
            {
                block = call.Closures.ExtractBlock();
            }
            else
            {
                block = CodeBlock.Parse(parser);
            }

            return new ForInStatement(pattern, expression, where, block);
        }

        public void Write(IWriter writer)
        {
            writer.WriteLine($"for {Pattern} in {Expression}{Where}");
            Block.Write(writer);
        }

    }
}
