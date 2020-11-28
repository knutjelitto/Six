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
            GenericArgumentClause? arguments = null;
            if (parser.Current == ToKind.Less && parser.Adjacent)
            {
                arguments = GenericArgumentClause.TryParse(parser);
            }

            return new TypeName(name, arguments ?? new GenericArgumentClause());
        }

        public override string ToString()
        {
            return $"{Name}{Arguments}";
        }
    }
}
