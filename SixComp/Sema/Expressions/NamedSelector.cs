using SixComp.Support;

namespace SixComp.Sema
{
    public class NamedSelector: Expression<Tree.ExplicitMemberExpression.NamedMemberSelector>
    {
        public NamedSelector(IScoped outer, Tree.ExplicitMemberExpression.NamedMemberSelector tree)
            : base(outer, tree)
        {
            Left = IExpression.Build(Outer, Tree.Left);
            Name = new FullName(Outer, Tree.Name);
            ArgumentNames = new BaseNames(Outer, tree.Names);
        }

        public IExpression Left { get; }
        public FullName Name { get; }
        public BaseNames ArgumentNames { get; }

        public override void Report(IWriter writer)
        {
            writer.WriteLine("select x.y<z>(a:)");
            using (writer.Indent())
            {
                Left.Report(writer);
                Name.Report(writer);
                ArgumentNames.Report(writer);
            }
        }
    }
}
