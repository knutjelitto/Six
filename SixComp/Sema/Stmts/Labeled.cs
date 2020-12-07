using SixComp.Support;

namespace SixComp.Sema
{
    public class Labeled : Base<Tree.LabeledStatement>, IStatement, INamedDeclaration
    {
        public Labeled(IScoped outer, Tree.LabeledStatement tree)
            : base(outer, tree)
        {
            Name = new BaseName(outer, tree.Label);
            Statement = IStatement.Build(outer, tree.Statement);

            Declare(this);
        }

        public BaseName Name { get; }
        public IStatement Statement { get; }

        public override void Resolve(IWriter writer)
        {
            Resolve(writer, Statement);
        }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Labeled))
            {
                Name.Report(writer, Strings.Head.Label);
                Statement.Report(writer, Strings.Head.Statement);
            }
        }
    }
}
