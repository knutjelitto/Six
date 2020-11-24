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
            var arguments = parser.Current == ToKind.Less && parser.Adjacent
                ? GenericArgumentClause.Parse(parser)
                : new GenericArgumentClause()
                ;

            return new NameExpression(name, arguments);
        }

        public override string ToString()
        {
            return $"{Name}{Arguments}";
        }
    }
}
