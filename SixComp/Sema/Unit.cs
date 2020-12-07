using SixComp.Support;

namespace SixComp.Sema
{
    public class Unit : Base<Tree.CompilationUnit>
    {
        public Unit(Context context, IScoped outer, Tree.CompilationUnit tree)
            : base(outer, tree)
        {
            Context = context;
        }

        public Context Context { get; }
        public Statements? Statements { get; private set; }

        public void Build()
        {
            Statements = new Statements(Outer, Tree.Statements);
        }

        public override void Resolve(IWriter writer)
        {
            Statements!.Resolve(writer);
        }

        public override void Report(IWriter writer)
        {
            using (writer.Indent($"unit {Short}:"))
            {
                foreach (var statement in Statements!)
                {
                    statement.Report(writer);
                    writer.WriteLine();
                }
            }
        }

        public string Short => Context.Short;
    }
}
