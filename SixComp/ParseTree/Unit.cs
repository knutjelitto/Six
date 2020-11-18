using SixComp.Support;

namespace SixComp.ParseTree
{
    public class Unit : IWritable
    {

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
            var declarations = StatementList.Parse(parser, new TokenSet(ToKind.EOF));

            return new Unit(declarations);
        }
    }
}
