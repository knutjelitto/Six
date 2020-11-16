namespace SixComp.ParseTree
{
    public class SelectExpression : AnyExpression
    {
        public SelectExpression(AnyExpression left, Name name)
        {
            Left = left;
            Name = name;
        }

        public AnyExpression Left { get; }
        public Name Name { get; }

        public static SelectExpression Parse(Parser parser, AnyExpression left)
        {
            parser.Consume(ToKind.Dot);

            var name = Name.Parse(parser);

            return new SelectExpression(left, name);
        }

        public override string ToString()
        {
            return $"{Left}.{Name}";
        }
    }
}
