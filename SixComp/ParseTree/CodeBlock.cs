using SixComp.Support;

namespace SixComp.ParseTree
{
    public class CodeBlock : SyntaxNode, AnyStatement
    {
        public static readonly TokenSet Firsts = new TokenSet(ToKind.LBrace);

        private CodeBlock(StatementList statements)
        {
            Statements = statements;
        }

        public StatementList Statements { get; }

        public static CodeBlock Parse(Parser parser)
        {
            parser.Consume(ToKind.LBrace);

            var statements = StatementList.Parse(parser, new TokenSet(ToKind.RBrace));

            parser.Consume(ToKind.RBrace);

            return new CodeBlock(statements);
        }

        public static CodeBlock From(StatementList statements)
        {
            return new CodeBlock(statements);
        }

        public void Write(IWriter writer)
        {
            using (writer.Block())
            {
                Statements.Write(writer);
            }
        }

        public override string ToString()
        {
            return $"{{ {Statements} }}";
        }
    }
}
