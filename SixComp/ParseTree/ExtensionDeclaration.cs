using SixComp.Support;

namespace SixComp.ParseTree
{
    public class ExtensionDeclaration : AnyDeclaration
    {
        public ExtensionDeclaration(Prefix prefix, TypeIdentifier name, TypeInheritanceClause inheritance, RequirementClause requirements, DeclarationList declarations)
        {
            Prefix = prefix;
            Name = name;
            Inheritance = inheritance;
            Requirements = requirements;
            Declarations = declarations;
        }

        public Prefix Prefix { get; }
        public TypeIdentifier Name { get; }
        public TypeInheritanceClause Inheritance { get; }
        public RequirementClause Requirements { get; }
        public DeclarationList Declarations { get; }

        public static ExtensionDeclaration Parse(Parser parser, Prefix prefix)
        {
            parser.Consume(ToKind.KwExtension);

            var name = TypeIdentifier.Parse(parser);
            var inheritance = parser.TryList(ToKind.Colon, TypeInheritanceClause.Parse);
            var requirements = parser.TryList(RequirementClause.Firsts, RequirementClause.Parse);
            parser.Consume(ToKind.LBrace);
            var declarations = DeclarationList.Parse(parser);
            parser.Consume(ToKind.RBrace);

            return new ExtensionDeclaration(prefix, name, inheritance, requirements, declarations);
        }

        public void Write(IWriter writer)
        {
            writer.WriteLine($"extension {Name}{Inheritance}");
            using (writer.Block())
            {
                Declarations.Write(writer);
            }
        }

    }
}
