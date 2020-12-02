using SixComp.Support;

namespace SixComp.Sema
{
    public class Labeled : Base<Tree.LabeledStatement>, IStatement
    {
        public Labeled(IScoped outer, Tree.LabeledStatement tree)
            : base(outer, tree)
        {
            Label = new BaseName(outer, tree.Label);
            Statement = IStatement.Build(outer, tree.Statement);
        }

        public BaseName Label { get; }
        public IStatement Statement { get; }

        public override void Report(IWriter writer)
        {
            writer.WriteLine(Strings.Labeled);
            using (writer.Indent())
            {
                Label.Report(writer);
                writer.Indent(() => Statement.Report(writer));
            }
        }
    }
}
