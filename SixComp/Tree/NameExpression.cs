namespace SixComp
{
    public partial class Tree
    {
        public class NameExpression : BaseExpression, AnyPrimaryExpression
        {
            public NameExpression(BaseName name, GenericArgumentClause arguments)
            {
                Name = name;
                Arguments = arguments;
            }

            public BaseName Name { get; }
            public GenericArgumentClause Arguments { get; }

            public static NameExpression Parse(Parser parser)
            {
                var name = BaseName.Parse(parser);
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
}