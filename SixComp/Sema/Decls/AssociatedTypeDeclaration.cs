using SixComp.Entities;
using SixComp.Support;

namespace SixComp.Sema
{
    public class AssociatedTypeDeclaration : Base<ParseTree.AssociatedTypeDeclaration>, INamedDeclaration, IWithRestrictions
    {
        public AssociatedTypeDeclaration(IScoped outer, ParseTree.AssociatedTypeDeclaration tree)
            : base(outer, tree)
        {
            Name = new BaseName(outer, tree.Name);
            Inheritance = new Inheritance(outer, tree.Inheritance);
            Type = ITypeDefinition.MaybeBuild(Outer, tree.Assignment);
            Where = new GenericRestrictions(this);
            Where.Add(this, Tree.Requirements);

            Declare(this);
        }

        public BaseName Name { get; }
        public Inheritance Inheritance { get; }
        public GenericRestrictions Where { get; }
        public ITypeDefinition? Type { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.AssociatedType))
            {
                Name.Report(writer, Strings.Head.Name);
                Inheritance.Report(writer);
                Where.Report(writer);
                Type.Report(writer, Strings.Head.Type);
            }
        }
    }
}
