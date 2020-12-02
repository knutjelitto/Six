using SixComp.Support;
using System.Collections.Generic;

namespace SixComp.Sema
{
    public class Unit : IReportable
    {
        public Unit(Context context, Tree.CompilationUnit tree)
        {
            Context = context;
            Tree = tree;
            Statements = new List<IStatement>();
        }

        public Context Context { get; }
        public Tree.CompilationUnit Tree { get; }
        public List<IStatement> Statements { get; }

        public void BuildTree(Package package)
        {
            foreach (var declaration in Tree.Statements)
            {
                var statement = IStatement.Build(package, declaration);
                Statements.Add(statement);
            }
        }

        public void Report(IWriter writer)
        {
            foreach (var statement in Statements)
            {
                statement.Report(writer);
                writer.WriteLine();
            }
        }

        public string Short => Context.Short;
    }
}
