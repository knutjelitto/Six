namespace SixComp.ParseTree
{
    public class TypeExpression : BaseExpression
    {
        public TypeExpression(AnyType type)
        {
            Type = type;
        }

        public AnyType Type { get; }

        public static TypeExpression Parse(Parser parser)
        {
            var type = AnyType.Parse(parser);

            return new TypeExpression(type);
        }

        public override string ToString()
        {
            return $"{Type}";
        }
    }
}
