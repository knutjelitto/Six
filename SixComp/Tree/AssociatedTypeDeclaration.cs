using SixComp.Support;

namespace SixComp.Tree
{
    public class AssociatedTypeDeclaration : AnyDeclaration
    {
        public AssociatedTypeDeclaration(Prefix prefix, BaseName name, TypeInheritanceClause inheritance, TypealiasAssignment? assignment, RequirementClause requirements)
        {
            Prefix = prefix;
            Name = name;
            Inheritance = inheritance;
            Assignment = assignment;
            Requirements = requirements;
        }

        public Prefix Prefix { get; }
        public BaseName Name { get; }
        public TypeInheritanceClause Inheritance { get; }
        public TypealiasAssignment? Assignment { get; }
        public RequirementClause Requirements { get; }

        public static AssociatedTypeDeclaration Parse(Parser parser, Prefix prefix)
        {
            // already parsed //parser.Consume(ToKind.KwAssociatedType);

            var name = BaseName.Parse(parser);
            var inheritance = parser.TryList(TypeInheritanceClause.Firsts, TypeInheritanceClause.Parse);
            var assignment = parser.Try(TypealiasAssignment.Firsts, TypealiasAssignment.Parse);
            var requirements = parser.TryList(RequirementClause.Firsts, RequirementClause.Parse);

            return new AssociatedTypeDeclaration(prefix, name, inheritance, assignment, requirements);
        }

        public void Write(IWriter writer)
        {
            writer.WriteLine($"{Prefix}{Name}{Inheritance}{Assignment}");
            Requirements.Write(writer);
        }
    }
}
