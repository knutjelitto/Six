using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class Statements : Items<IStatement, Tree.StatementList>, IStatement
    {
        public Statements(IScoped outer, Tree.StatementList tree)
            : base(outer, tree, Enum(outer, tree))
        {
        }

        public override void Resolve(IWriter writer)
        {
            ResolveItems(writer);
        }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.Statements);
        }

        private static IEnumerable<IStatement> Enum(IScoped outer, Tree.StatementList tree)
        {
            return tree.Select(statement => IStatement.Build(outer, statement));
        }
    }
}
