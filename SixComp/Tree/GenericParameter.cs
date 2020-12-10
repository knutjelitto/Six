namespace SixComp
{
    public partial class ParseTree
    {
        public class GenericParameter
        {
            public GenericParameter(BaseName name, GenericRequirement? requirement)
            {
                Name = name;
                Requirement = requirement;
            }

            public BaseName Name { get; }
            public GenericRequirement? Requirement { get; }

            public static GenericParameter Parse(Parser parser)
            {
                var name = BaseName.Parse(parser);

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
}