using SixComp.Support;

namespace SixComp.Sema
{
    public class StructDeclaration : Base<Tree.StructDeclaration>, IDeclaration, IWhere, INamed
    {
        public StructDeclaration(IScoped outer, Tree.StructDeclaration tree)
            : base(outer, tree)
        {
            Name = new BaseName(Outer, Tree.Name);
            Where = new GenericRestrictions(this);
            GenericParameters = new GenericParameters(this, Tree.Generics);
            Inheritance = new Inheritance(Outer, Tree.Inheritance);
            Where.Add(this, Tree.Requirements);
            Declarations = new Declarations(this, Tree.Declarations);
        }

        public BaseName Name { get; }
        public GenericParameters GenericParameters { get; }
        public Inheritance Inheritance { get; }
        public GenericRestrictions Where { get; }
        public Declarations Declarations { get; }

        public override void Report(IWriter writer)
        {
            Tree.Tree(writer);
            using (writer.Indent(Strings.Head.Struct))
            {
                Name.Report(writer, Strings.Head.Name);
                GenericParameters.Report(writer);
                Inheritance.Report(writer);
                Where.Report(writer);
                Declarations.Report(writer);
            }
        }
    }
}
