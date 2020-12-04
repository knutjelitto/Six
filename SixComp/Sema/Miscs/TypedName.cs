using SixComp.Support;

namespace SixComp.Sema
{
    public class TypedName : Base<Tree.BaseName>
    {
        public TypedName(IScoped outer, Tree.BaseName tree, Tree.AnyType? treeType)
            : base(outer, tree)
        {
            Name = new BaseName(outer, tree);
            Type = IType.MaybeBuild(outer, treeType);
            TreeType = treeType;
        }

        public BaseName Name { get; }
        public IType? Type { get; }
        public Tree.AnyType? TreeType { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.TypedName))
            {
                Name.Report(writer, Strings.Head.Name);
                Type.Report(writer, Strings.Head.Type);
            }
        }
    }
}
