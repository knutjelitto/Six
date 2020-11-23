namespace SixComp.ParseTree
{
    public class IsPattern : SyntaxNode, AnyPattern
    {
        public IsPattern(AnyType type)
        {
            Type = type;
        }

        public AnyType Type { get; }

        public static IsPattern Parse(Parser parser)
        {
            parser.Consume(ToKind.KwIs);
            var type = AnyType.Parse(parser);

            return new IsPattern(type);
        }
    }
}
