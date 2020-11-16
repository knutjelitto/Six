namespace SixComp.ParseTree
{
    public class TypeName : AnyType
    {
        public TypeName(Name name, GenericArgumentClause arguments)
        {
            Name = name;
            Arguments = arguments;
        }

        public Name Name { get; }
     
        public GenericArgumentClause Arguments { get; }

        public static TypeName Parse(Parser parser)
        {
            var name = Name.Parse(parser);
            var arguments = parser.TryList(ToKind.Less, GenericArgumentClause.Parse);

            return new TypeName(name, arguments);
        }

        public override string ToString()
        {
            return $"{Name}{Arguments}";
        }
    }
}
