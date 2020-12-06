using SixComp.Support;
using System.Collections.Generic;

namespace SixComp.Sema
{
    public class Unit : Base<Tree.CompilationUnit>
    {
        public Unit(Context context, IScoped outer, Tree.CompilationUnit tree)
            : base(outer, tree)
        {
            Context = context;
            Statements = new List<IStatement>();
        }

        public Context Context { get; }
        public List<IStatement> Statements { get; }

        public void BuildTree(Module module)
        {
            foreach (var declaration in Tree.Statements)
            {
                var statement = IStatement.Build(module, declaration);
                Statements.Add(statement);
            }
        }

        public override void Report(IWriter writer)
        {
            using (writer.Indent($"unit {Short}:"))
            {
                foreach (var statement in Statements)
                {
                    statement.Report(writer);
                    writer.WriteLine();
                }
            }
        }

        public string Short => Context.Short;
    }
}
