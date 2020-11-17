using SixComp.Support;
using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class StatementList : ItemList<AnyStatement>
    {
        public StatementList(List<AnyStatement> statements) : base(statements) { }
        public StatementList() { }

        public static StatementList Parse(Parser parser, TokenSet follow)
        {
            var statements = new List<AnyStatement>();

            while (!follow.Contains(parser.Current))
            {
                var statement = AnyStatement.Parse(parser);

                statements.Add(statement);

                while (parser.Current == ToKind.Semi)
                {
                    parser.ConsumeAny();
                }
            }

            return new StatementList(statements);
        }
    }
}
