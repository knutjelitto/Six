using SixComp.Support;

namespace SixComp.Tree
{
    public class EnumDeclaration : AnyDeclaration
    {
        public EnumDeclaration(Prefix prefix, BaseName name, GenericParameterClause generics, TypeInheritanceClause inheritance, RequirementClause requirements, DeclarationClause declarations)
        {
            Prefix = prefix;
            Name = name;
            Generics = generics;
            Inheritance = inheritance;
            Requirements = requirements;
            Declarations = declarations;
        }

        public Prefix Prefix { get; }
        public BaseName Name { get; }
        public GenericParameterClause Generics { get; }
        public TypeInheritanceClause Inheritance { get; }
        public RequirementClause Requirements { get; }
        public DeclarationClause Declarations { get; }

        public static EnumDeclaration Parse(Parser parser, Prefix prefix)
        {
            // already parsed //parser.Consume(ToKind.KwEnum);

            var name = BaseName.Parse(parser);
            var generics = parser.TryList(ToKind.Less, GenericParameterClause.Parse);
            var inheritance = parser.TryList(ToKind.Colon, TypeInheritanceClause.Parse);
            var requirements = parser.TryList(RequirementClause.Firsts, RequirementClause.Parse);
            var declarations = DeclarationClause.Parse(parser, AnyDeclaration.Context.Enum);

            return new EnumDeclaration(prefix, name, generics, inheritance, requirements, declarations);
        }

        public void Write(IWriter writer)
        {
            Prefix.Write(writer);
            writer.WriteLine($"{Name}{Generics}{Inheritance}{Requirements}");
            Declarations.Write(writer);
        }

        public override string ToString()
        {
            return $"{Prefix}{Name}{Generics}{Inheritance}{Requirements}{Declarations}";
        }
    }
}
