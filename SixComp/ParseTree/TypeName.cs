namespace SixComp.ParseTree
{
    public class TypeName : IType
    {
        public TypeName(Name name, TypeList arguments)
        {
            Name = name;
            Arguments = arguments;
        }

        public Name Name { get; }
     
        public TypeList Arguments { get; }

        public static TypeName Parse(Parser parser)
        {
            var name = Name.Parse(parser);
            var arguments = parser.TryList(ToKind.Less, ParseGenericArgumentClause);

            return new TypeName(name, arguments);
        }

        public override string ToString()
        {
            if (Arguments.Count > 0)
            {
                return $"{Name}<{Arguments}>";
            }
            return $"{Name}";
        }
    }
}
