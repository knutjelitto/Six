namespace SixComp.ParseTree
{
    public class AsPattern : SyntaxNode, AnyPattern
    {
        public AsPattern(AnyPattern pattern, AnyType type)
        {
            Pattern = pattern;
            Type = type;
        }

        public AnyPattern Pattern { get; }
        public AnyType Type { get; }

        public static AsPattern Parse(Parser parser, AnyPattern pattern)
        {
            parser.Consume(ToKind.KwAs);
            var type = AnyType.Parse(parser);

            return new AsPattern(pattern, type);
        }
    }
}
