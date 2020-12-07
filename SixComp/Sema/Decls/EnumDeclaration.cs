using SixComp.Support;

namespace SixComp.Sema
{
    public class EnumDeclaration : Nominal<Tree.EnumDeclaration>
    {
        public EnumDeclaration(IScoped outer, Tree.EnumDeclaration tree)
            : base(outer, tree, tree.Name)
        {
            Generics = new GenericParameters(this, Tree.Generics);
            Inheritance = new Inheritance(Outer, Tree.Inheritance);
            Where.Add(this, Tree.Requirements);
            Declarations = new Declarations(this, Tree.Declarations);

            Declare(this);
        }

        public override GenericParameters Generics { get; }
        public Inheritance Inheritance { get; }
        public override Declarations Declarations { get; }

        public override void Resolve(IWriter writer)
        {
            Resolve(writer, Generics, Inheritance, Where, Declarations);
        }

        public override void Report(IWriter writer)
        {
            Tree.Tree(writer);
            using (writer.Indent(Strings.Head.Enum))
            {
                Name.Report(writer, Strings.Head.Name);
                Generics.Report(writer);
                Inheritance.Report(writer);
                Where.Report(writer);
                Declarations.Report(writer);
            }
        }
    }
}
