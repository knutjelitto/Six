namespace SixComp.ParseTree
{
    public class AssociatedTypeDeclaration : AnyDeclaration
    {
        public AssociatedTypeDeclaration(Prefix prefix, Name name, TypeInheritanceClause inheritance, TypealiasAssignment? assignment, RequirementClause requirements)
        {
            Prefix = prefix;
            Name = name;
            Inheritance = inheritance;
            Assignment = assignment;
            Requirements = requirements;
        }

        public Prefix Prefix { get; }
        public Name Name { get; }
        public TypeInheritanceClause Inheritance { get; }
        public TypealiasAssignment? Assignment { get; }
        public RequirementClause Requirements { get; }

        public static AssociatedTypeDeclaration Parse(Parser parser, Prefix prefix)
        {
            // already parsed //parser.Consume(ToKind.KwAssociatedType);

            var name = Name.Parse(parser);
            var inheritance = parser.TryList(TypeInheritanceClause.Firsts, TypeInheritanceClause.Parse);
            var assignment = parser.Try(TypealiasAssignment.Firsts, TypealiasAssignment.Parse);
            var requirements = parser.TryList(RequirementClause.Firsts, RequirementClause.Parse);

            return new AssociatedTypeDeclaration(prefix, name, inheritance, assignment, requirements);
        }
    }
}
