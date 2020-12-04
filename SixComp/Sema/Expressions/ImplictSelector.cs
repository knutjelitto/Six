using SixComp.Support;

namespace SixComp.Sema
{
    public class ImplicitSelector: Expression<Tree.ImplicitMemberExpression>
    {
        public ImplicitSelector(IScoped outer, Tree.ImplicitMemberExpression tree)
            : base(outer, tree)
        {
            Name = new BaseName(Outer, Tree.Name);
            Operator = new BaseName(Outer, tree.Operator);
        }

        public BaseName Name { get; }
        public BaseName Operator { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Select))
            {
                Operator.Report(writer, Strings.Head.Operator);
                Name.Report(writer, Strings.Head.Name);
            }
        }
    }
}
