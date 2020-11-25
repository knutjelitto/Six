using SixComp.Support;

namespace SixComp.ParseTree
{
    public class Unit : IWritable
    {
        public static TokenSet Follows = new TokenSet(ToKind.EOF);

        public Unit(StatementList statements)
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

        public static Unit Parse(Parser parser)
        {
            var declarations = StatementList.Parse(parser, Follows);

            return new Unit(declarations);
        }
    }
}
