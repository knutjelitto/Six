using Six.Support;

namespace SixComp
{
    public partial class ParseTree
    {
        public class ExtensionDeclaration : IDeclaration
        {
            public ExtensionDeclaration(Prefix prefix, TypeIdentifier name, TypeInheritanceClause inheritance, RequirementClause requirements, DeclarationClause declarations)
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
            public DeclarationClause Declarations { get; }

            public static ExtensionDeclaration Parse(Parser parser, Prefix prefix)
            {
                // already parsed //parser.Consume(ToKind.KwExtension);

                var name = TypeIdentifier.Parse(parser);
                var inheritance = parser.TryList(ToKind.Colon, TypeInheritanceClause.Parse);
                var requirements = parser.TryList(RequirementClause.Firsts, RequirementClause.Parse);
                var declarations = DeclarationClause.Parse(parser, IDeclaration.Context.Extension);

                return new ExtensionDeclaration(prefix, name, inheritance, requirements, declarations);
            }

            public void Write(IWriter writer)
            {
                writer.WriteLine($"extension {Name}{Inheritance}");
                Requirements.Write(writer);
                Declarations.Write(writer);
            }
        }
    }
}