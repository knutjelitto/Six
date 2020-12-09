namespace SixComp
{
    public partial class Tree
    {
        public class ImplicitMemberExpression : BaseExpression, AnyPrimaryExpression
        {
            public ImplicitMemberExpression(Token op, BaseName name)
            {
                Name = name;
                Operator = BaseName.From(op);
            }

            public BaseName Name { get; }
            public BaseName Operator { get; }

            public static ImplicitMemberExpression Parse(Parser parser)
            {
                var op = parser.Consume(ToKind.Dot);

                var name = BaseName.Parse(parser);

                return new ImplicitMemberExpression(op, name);
            }

            public override string ToString()
            {
                return $"{Operator}{Name}";
            }
        }
    }
}