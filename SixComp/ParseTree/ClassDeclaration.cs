using SixComp.Support;

namespace SixComp.ParseTree
{
    public class ClassDeclaration : Declaration
    {
        public ClassDeclaration(Name name, DeclarationList declarations)
        {
            Name = name;
            Declarations = declarations;
        }

        public Name Name { get; }
        public DeclarationList Declarations { get; }

        public override void Write(IWriter writer)
        {
            writer.WriteLine($"class {Name}");
            Declarations.Write(writer);
        }
    }
}
