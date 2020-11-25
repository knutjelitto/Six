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
                CcBlock.Ignore(parser);
                var statement = AnyStatement.TryParse(parser);

                if (statement == null)
                {
                    break;
                }
                statements.Add(statement);
                while (parser.Match(ToKind.SemiColon))
                {
                    ;
                }
            }

            if (statements.Count == 0)
            {
                return new StatementList();
            }

            return new StatementList(statements);
        }
    }
}
