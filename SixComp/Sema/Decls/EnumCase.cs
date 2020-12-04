using SixComp.Support;

namespace SixComp.Sema
{
    public class EnumCase : Base<Tree.EnumCaseItem>, IDeclaration, INamed
    {
        public EnumCase(IScoped outer, Tree.Prefix prefix, Tree.EnumCaseItem tree)
            : base(outer, tree)
        {
            Prefix = prefix;

            Name = new BaseName(Outer, tree.Name);
            Tuple = (TupleType?)IType.MaybeBuild(Outer, tree.Tuple);
            Initializer = IExpression.MaybeBuild(outer, tree.Initializer);
        }

        public Tree.Prefix Prefix { get; }

        public BaseName Name { get; }
        public TupleType? Tuple { get; }
        public IExpression? Initializer { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Case))
            {
                Name.Report(writer, Strings.Head.Name);
                Tuple?.Report(writer);
                Initializer.Report(writer, Strings.Head.Initializer);
            }
        }
    }
}
