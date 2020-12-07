using SixComp.Support;

namespace SixComp.Sema
{
    public class TypealiasDeclaration : Nominal<Tree.TypealiasDeclaration>
    {
        public TypealiasDeclaration(IScoped outer, Tree.TypealiasDeclaration tree)
            : base(outer, tree, tree.Name)
        {
            Generics = new GenericParameters(this, Tree.Parameters);
            Where.Add(this, Tree.Requirements);
            Type = ITypeDefinition.Build(Outer, tree.Assignment);
            Declarations = new Declarations(this);

            Declare(this);
        }

        public override GenericParameters Generics { get; }
        public ITypeDefinition Type { get; }
        public override Declarations Declarations { get; }

        public override void Resolve(IWriter writer)
        {
            Resolve(writer, Generics, Where, Type);
        }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Typealias))
            {
                Name.Report(writer, Strings.Head.Name);
                Generics.Report(writer);
                Where.Report(writer);
                Type.Report(writer, Strings.Head.Type);
            }
        }
    }
}
