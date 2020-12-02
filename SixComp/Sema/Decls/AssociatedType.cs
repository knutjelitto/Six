using SixComp.Support;

namespace SixComp.Sema
{
    public class AssociatedType : Base<Tree.AssociatedTypeDeclaration>, IDeclaration, IOwner
    {
        public AssociatedType(IScoped outer, Tree.AssociatedTypeDeclaration tree)
            : base(outer, tree)
        {
            Name = new BaseName(outer, tree.Name);
            Inheritance = new Inheritance(outer, tree.Inheritance);
            Where = new GenericRestrictions(this);
            Where.Add(this, Tree.Requirements);
            Type = IType.MaybeBuild(Outer, tree.Assignment);
        }

        public BaseName Name { get; }
        public Inheritance Inheritance { get; }
        public GenericRestrictions Where { get; }
        public IType? Type { get; }

        public override void Report(IWriter writer)
        {
            writer.WriteLine(Strings.Typealias);
            using (writer.Indent())
            {
                Name.Report(writer, Strings.Head.Name);
                Inheritance.Report(writer);
                Where.Report(writer);
                Type.Report(writer, Strings.Head.Type);
            }
        }
    }
}
