using SixComp.Support;

namespace SixComp.Tree
{
    public class TypealiasDeclaration : AnyDeclaration, INominal
    {
        public TypealiasDeclaration(Prefix prefix, BaseName name, GenericParameterClause generics, TypealiasAssignment assignment, RequirementClause requirements)
        {
            Prefix = prefix;
            Name = name;
            Generics = generics;
            Assignment = assignment;
            Requirements = requirements;
        }

        public Prefix Prefix { get; }
        public BaseName Name { get; }
        public GenericParameterClause Generics { get; }
        public TypealiasAssignment Assignment { get; }
        public RequirementClause Requirements { get; }

        public static TypealiasDeclaration Parse(Parser parser, Prefix prefix)
        {
            // already parsed //parser.Consume(ToKind.KwTypealias);

            var name = BaseName.Parse(parser);
            var parameters = parser.TryList(ToKind.Less, GenericParameterClause.Parse);
            var assignment = TypealiasAssignment.Parse(parser);
            var requirements = parser.TryList(RequirementClause.Firsts, RequirementClause.Parse);

            return new TypealiasDeclaration(prefix, name, parameters, assignment, requirements);
        }

        public void Write(IWriter writer)
        {
            writer.WriteLine($"typealias {Name}{Generics}{Assignment}");
            Requirements.Write(writer);
        }
    }
}
