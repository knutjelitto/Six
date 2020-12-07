using SixComp.Support;
using System.Linq;

namespace SixComp.Sema
{
    public class ExplicitSelectorExpression: Base<Tree.ExplicitMemberExpression.NamedMemberSelector>, IExpression
    {
        public ExplicitSelectorExpression(IScoped outer, Tree.ExplicitMemberExpression.NamedMemberSelector tree)
            : base(outer, tree)
        {
            Left = IExpression.Build(Outer, Tree.Left);
            Name = new FullName(Outer, Tree.Name);
            Operator = new BaseName(Outer, tree.Operator);
            ArgumentNames = new BaseNames(Outer, tree.Names.Names.Select(n => n.Name));
        }

        public IExpression Left { get; }
        public FullName Name { get; }
        public BaseName Operator { get; }
        public BaseNames ArgumentNames { get; }

        public override void Resolve(IWriter writer)
        {
            // TODO: argument-names should select overload
            Resolve(writer, Left);
        }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Select))
            {
                Left.Report(writer, Strings.Head.Left);
                Operator.Report(writer, Strings.Head.Operator);
                Name.Report(writer, Strings.Head.Name);
                ArgumentNames.Report(writer);
            }
        }
    }
}
