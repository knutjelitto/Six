using SixComp.Support;

namespace SixComp
{
    public partial class ParseTree
    {
        public class CompilationUnit : IWritable
        {
            public static TokenSet Follows = new TokenSet(ToKind.EOF);

            public CompilationUnit(StatementList statements)
            {
                Statements = statements;
            }

            public StatementList Statements { get; }

            public void Write(IWriter writer)
            {
                bool more = false;

                foreach (var statement in Statements)
                {
                    if (more)
                    {
                        writer.WriteLine();
                    }
                    statement.Write(writer);
                    more = true;
                }
            }

            public static CompilationUnit Parse(Parser parser)
            {
                var declarations = StatementList.Parse(parser, Follows);

                parser.Consume(ToKind.EOF);

                return new CompilationUnit(declarations);
            }
        }
    }
}