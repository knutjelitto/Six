namespace SixComp.ParseTree
{
    public class NameExpression : BaseExpression, AnyPrimaryExpression
    {
        public NameExpression(Name name, GenericArgumentClause arguments)
        {
            Name = name;
            Arguments = arguments;
        }

        public Name Name { get; }
        public GenericArgumentClause Arguments { get; }

        public static NameExpression Parse(Parser parser)
        {
            var name = Name.Parse(parser);
            GenericArgumentClause? arguments = null;
            if (parser.Current == ToKind.Less && parser.Adjacent)
            {
                arguments = GenericArgumentClause.TryParse(parser);
            }

            return new NameExpression(name, arguments ?? new GenericArgumentClause());
        }

        public override string ToString()
        {
            return $"{Name}{Arguments}";
        }
    }
}
