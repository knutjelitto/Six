using SixComp.Support;

namespace SixComp.ParseTree
{
    public class StructDeclaration : AnyDeclaration
    {
        public StructDeclaration(Prefix prefix, Name name, GenericParameterClause generics, TypeInheritanceClause inheritance, RequirementClause requirements, DeclarationClause declarations)
        {
            Prefix = prefix;
            Name = name;
            Generics = generics;
            Inheritance = inheritance;
            Requirements = requirements;
            Declarations = declarations;
        }

        public Prefix Prefix { get; }
        public Name Name { get; }
        public GenericParameterClause Generics { get; }
        public TypeInheritanceClause Inheritance { get; }
        public RequirementClause Requirements { get; }
        public DeclarationClause Declarations { get; }

        public static StructDeclaration Parse(Parser parser, Prefix prefix)
        {
            parser.Consume(ToKind.KwStruct);
            var name = Name.Parse(parser);
            var generics = parser.TryList(ToKind.Less, GenericParameterClause.Parse);
            var inheritance = parser.TryList(ToKind.Colon, TypeInheritanceClause.Parse);
            var requirements = parser.TryList(RequirementClause.Firsts, RequirementClause.Parse);
            var declarations = DeclarationClause.Parse(parser);

            return new StructDeclaration(prefix, name, generics, inheritance, requirements, declarations);
        }

        public void Write(IWriter writer)
        {
            writer.WriteLine($"{Prefix}struct {Name}{Generics}{Inheritance}{Requirements}");
            Declarations.Write(writer);
        }
    }
}
