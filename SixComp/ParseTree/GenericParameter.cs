namespace SixComp.ParseTree
{
    public class GenericParameter
    {
        public GenericParameter(Name name, GenericRequirement? requirement)
        {
            Name = name;
            Requirement = requirement;
        }

        public Name Name { get; }
        public GenericRequirement? Requirement { get; }

        public static GenericParameter Parse(Parser parser)
        {
            var name = Name.Parse(parser);

            if (parser.Current == ToKind.Colon)
            {
                var requirement = GenericRequirement.Parse(parser);
                return new GenericParameter(name, requirement);
            }

            return new GenericParameter(name, null);
        }

        public override string ToString()
        {
            return $"{Name}{Requirement}";
        }
    }
}
