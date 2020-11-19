namespace SixComp.ParseTree
{
    public class TypealiasDeclaration : AnyDeclaration
    {
        public TypealiasDeclaration(Prefix prefix, Name name, GenericParameterClause parameters, TypealiasAssignment assignment)
        {
            Prefix = prefix;
            Name = name;
            Parameters = parameters;
            Assignment = assignment;
        }

        public Prefix Prefix { get; }
        public Name Name { get; }
        public GenericParameterClause Parameters { get; }
        public TypealiasAssignment Assignment { get; }

        public static TypealiasDeclaration Parse(Parser parser, Prefix prefix)
        {
            parser.Consume(ToKind.KwTypealias);

            var name = Name.Parse(parser);
            var parameters = parser.TryList(ToKind.Less, GenericParameterClause.Parse);
            var assignment = TypealiasAssignment.Parse(parser);

            return new TypealiasDeclaration(prefix, name, parameters, assignment);
        }
    }
}
