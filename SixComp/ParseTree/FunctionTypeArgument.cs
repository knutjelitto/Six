namespace SixComp.ParseTree
{
    public class FunctionTypeArgument
    {
        public FunctionTypeArgument(AnyType type)
        {
            Type = type;
        }

        public AnyType Type { get; }

        public static FunctionTypeArgument Parse(Parser parser)
        {
            var type = parser.Current == ToKind.Name && parser.Next == ToKind.Colon
                ? (AnyType)LabeledType.Parse(parser)
                : PrefixedType.Parse(parser)
                ;

            return new FunctionTypeArgument(type);
        }

        public override string ToString()
        {
            return $"{Type}";
        }
    }
}
