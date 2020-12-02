namespace SixComp.Tree
{
    public class ImplicitMemberExpression : BaseExpression, AnyPrimaryExpression
    {
        public ImplicitMemberExpression(BaseName name)
        {
            Name = name;
        }

        public BaseName Name { get; }

        public static ImplicitMemberExpression Parse(Parser parser)
        {
            parser.Consume(ToKind.Dot);

            var name = BaseName.Parse(parser);

            return new ImplicitMemberExpression(name);
        }

        public override string ToString()
        {
            return $".{Name}";
        }
    }
}
