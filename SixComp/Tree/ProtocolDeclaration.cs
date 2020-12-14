using Six.Support;

namespace SixComp
{
    public partial class ParseTree
    {
        public class ProtocolDeclaration : IDeclaration, INominalWhithDeclarations
        {
            public ProtocolDeclaration(Prefix prefix, BaseName name, GenericParameterClause generics, TypeInheritanceClause inheritance, RequirementClause requirements, DeclarationClause declarations)
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

            public static ProtocolDeclaration Parse(Parser parser, Prefix prefix)
            {
                // already parsed //parser.Consume(ToKind.KwProtocol);

                var name = BaseName.Parse(parser);
                var parameters = parser.TryList(ToKind.Less, GenericParameterClause.Parse);
                var inheritance = parser.TryList(ToKind.Colon, TypeInheritanceClause.Parse);
                var requirements = parser.TryList(RequirementClause.Firsts, RequirementClause.Parse);
                var declarations = DeclarationClause.Parse(parser, IDeclaration.Context.Protocol);

                return new ProtocolDeclaration(prefix, name, parameters, inheritance, requirements, declarations);
            }

            public void Write(IWriter writer)
            {
                Prefix.Write(writer);
                writer.WriteLine($"{Name}{Generics}{Inheritance}{Requirements}");
                Declarations.Write(writer);
            }
        }
    }
}