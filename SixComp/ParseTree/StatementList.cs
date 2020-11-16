using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class StatementList : ItemList<AnyStatement>
    {
        public StatementList(List<AnyStatement> statements) : base(statements) { }

        public StatementList() { }

        public static StatementList Parse(Parser parser)
        {
            var statements = new List<AnyStatement>();

            while (parser.Current.Kind != ToKind.RBrace)
            {
                var statement = AnyStatement.Parse(parser);

                statements.Add(statement);

                while (parser.Current.Kind == ToKind.Semi)
                {
                    parser.Consume();
                }
            }

            return new StatementList(statements);
        }
    }
}
