namespace SixComp.Tree
{
    public class FullName : AnyType, AnyExpression
    {
        public FullName(BaseName name, GenericArgumentClause arguments)
        {
            Name = name;
            Arguments = arguments;
        }

        public BaseName Name { get; }
     
        public GenericArgumentClause Arguments { get; }

        public AnyExpression? LastExpression => this;

        public static FullName Parse(Parser parser)
        {
            var name = BaseName.Parse(parser);
            GenericArgumentClause? arguments = null;
            if (parser.Current == ToKind.Less && parser.Adjacent)
            {
                arguments = GenericArgumentClause.TryParse(parser);
            }

            return new FullName(name, arguments ?? new GenericArgumentClause());
        }

        public override string ToString()
        {
            return $"{Name}{Arguments}";
        }
    }
}
