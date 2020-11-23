namespace SixComp.ParseTree
{
    public class OptionalPattern : SyntaxNode, AnyPattern
    {
        public OptionalPattern(AnyPattern pattern)
        {
            Pattern = pattern;
        }

        public AnyPattern Pattern { get; }

        public static OptionalPattern Parse(Parser parser, AnyPattern pattern)
        {
            parser.Consume(ToKind.Quest);

            return new OptionalPattern(pattern);
        }
    }
}
