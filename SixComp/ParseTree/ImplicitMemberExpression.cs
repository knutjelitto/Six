namespace SixComp.ParseTree
{
    public class ImplicitMemberExpression : BaseExpression, AnyPrimaryExpression
    {
        public ImplicitMemberExpression(Name name)
        {
            Name = name;
        }

        public Name Name { get; }

        public static ImplicitMemberExpression Parse(Parser parser)
        {
            parser.Consume(ToKind.Dot);

            var name = Name.Parse(parser);

            return new ImplicitMemberExpression(name);
        }

        public override string ToString()
        {
            return $".{Name}";
        }
    }
}
