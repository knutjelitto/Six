using SixComp.Support;

namespace SixComp.Sema
{
    public class BlockVar : Base<Tree.VarDeclaration>, IDeclaration, INamed
    {
        public BlockVar(IScoped outer, Tree.VarDeclaration tree)
            : base(outer, tree)
        {
            Name = new BaseName(outer, tree.Name);
            Type = IType.MaybeBuild(outer, tree.Type);
        }

        public BaseName Name { get; }
        public IType? Type { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Var))
            {
                Name.Report(writer, Strings.Head.Name);
                Type.Report(writer, Strings.Head.Type);
                writer.WriteLine(Strings.Incomplete);
            }
        }
    }
}
