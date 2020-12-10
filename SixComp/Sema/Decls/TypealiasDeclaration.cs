using SixComp.Support;

namespace SixComp.Sema
{
    public class TypealiasDeclaration : Nominal<ParseTree.TypealiasDeclaration>
    {
        public TypealiasDeclaration(IScoped outer, ParseTree.TypealiasDeclaration tree)
            : base(outer, tree)
        {
            Type = ITypeDefinition.Build(Outer, tree.Assignment);

            Declare(this);
        }

        public ITypeDefinition Type { get; }

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
