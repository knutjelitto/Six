using SixComp.Support;

namespace SixComp.ParseTree
{
    public class StructDeclaration : Declaration
    {
        public StructDeclaration(Name name, GenericParameterList parameters, DeclarationList declarations)
        {
            Name = name;
            Parameters = parameters;
            Declarations = declarations;
        }

        public Name Name { get; }
        public GenericParameterList Parameters { get; }
        public DeclarationList Declarations { get; }

        public override void Write(IWriter writer)
        {
            writer.WriteLine($"struct {Name}{Parameters}");
            using (writer.Block())
            {
                Declarations.Write(writer);
            }
        }
    }
}
