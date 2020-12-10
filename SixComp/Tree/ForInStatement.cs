using SixComp.Support;
using System;
using System.Diagnostics;

namespace SixComp
{
    public partial class ParseTree
    {
        public class ForInStatement : SyntaxNode, IWritable, IStatement
        {
            public ForInStatement(IPattern pattern, IExpression expression, WhereClause? where, CodeBlock block)
            {
                Pattern = pattern;
                Expression = expression;
                Where = where;
                Block = block;
            }

            public IPattern Pattern { get; }
            public IExpression Expression { get; }
            public WhereClause? Where { get; }
            public CodeBlock Block { get; }

            public static ForInStatement Parse(Parser parser)
            {
                parser.Consume(ToKind.KwFor);
                var withCase = parser.Match(ToKind.KwCase);
                Debug.Assert(!withCase);
                var pattern = IPattern.Parse(parser);
                var type = parser.Try(TypeAnnotation.Firsts, TypeAnnotation.Parse);
                parser.Consume(ToKind.KwIn);
                var expression = IExpression.TryParse(parser) ?? throw new InvalidOperationException($"{typeof(ForInStatement)}");
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

            public override string ToString()
            {
                return $"for {Pattern} in {Expression}{Where} {Block}";
            }
        }
    }
}