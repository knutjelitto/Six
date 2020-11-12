using SixComp.Support;

namespace SixComp.ParseTree
{
    public class StructDeclaration : Declaration
    {
        public StructDeclaration(Name name, DeclarationList declarations)
        {
            Name = name;
            Declarations = declarations;
        }

        public Name Name { get; }
        public DeclarationList Declarations { get; }

        public override void Write(IWriter writer)
        {
            writer.WriteLine($"struct {Name}");
            Declarations.Write(writer);
        }
    }
}
