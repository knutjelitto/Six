using Six.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class Statements : Items<IStatement, ParseTree.StatementList>, IStatement
    {
        public Statements(IScoped outer, ParseTree.StatementList tree)
            : base(outer, tree, Enum(outer, tree))
        {
        }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.Statements);
        }

        private static IEnumerable<IStatement> Enum(IScoped outer, ParseTree.StatementList tree)
        {
            return tree.Select(statement => IStatement.Build(outer, statement));
        }
    }
}
