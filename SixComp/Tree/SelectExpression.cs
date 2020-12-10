namespace SixComp
{
    public partial class ParseTree
    {
        public class SelectExpression : BaseExpression, IExpression
        {
            public SelectExpression(IExpression left, BaseName name)
            {
                Left = left;
                Name = name;
            }

            public IExpression Left { get; }
            public BaseName Name { get; }

            public static SelectExpression Parse(Parser parser, IExpression left)
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