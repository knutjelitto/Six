namespace SixComp
{
    public partial class ParseTree
    {
        public class OptionalPattern : SyntaxNode, IPattern
        {
            public OptionalPattern(IPattern pattern)
            {
                Pattern = pattern;
            }

            public IPattern Pattern { get; }

            public static OptionalPattern Parse(Parser parser, IPattern pattern)
            {
                parser.Consume(ToKind.Quest);

                return new OptionalPattern(pattern);
            }

            public override string ToString()
            {
                return $"{Pattern}?";
            }
        }
    }
}