namespace SixComp
{
    public partial class Tree
    {
        public class SelectExpression : BaseExpression, AnyExpression
        {
            public SelectExpression(AnyExpression left, BaseName name)
            {
                Left = left;
                Name = name;
            }

            public AnyExpression Left { get; }
            public BaseName Name { get; }

            public static SelectExpression Parse(Parser parser, AnyExpression left)
            {
                parser.Consume(ToKind.Dot);

                var name = BaseName.Parse(parser);

                return new SelectExpression(left, name);
            }

            public override string ToString()
            {
                return $"{Left}.{Name}";
            }
        }
    }
}