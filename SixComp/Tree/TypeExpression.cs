namespace SixComp
{
    public partial class ParseTree
    {
        public class TypeExpression : BaseExpression
        {
            public TypeExpression(IType type)
            {
                Type = type;
            }

            public IType Type { get; }

            public static TypeExpression Parse(Parser parser)
            {
                var type = IType.Parse(parser);

                return new TypeExpression(type);
            }

            public override string ToString()
            {
                return $"{Type}";
            }
        }
    }
}