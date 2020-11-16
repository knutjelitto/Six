using SixComp.Support;

namespace SixComp.ParseTree
{
    public class CodeBlock : IWriteable
    {
        public CodeBlock(StatementList statements)
        {
            Statements = statements;
        }

        public StatementList Statements { get; }

        public static CodeBlock Parse(Parser parser)
        {
            parser.Consume(ToKind.LBrace);

            var statements = StatementList.Parse(parser);

            parser.Consume(ToKind.RBrace);

            return new CodeBlock(statements);
        }

        public void Write(IWriter writer)
        {
            using (writer.Block())
            {
                Statements.Write(writer);
            }
        }
    }
}
