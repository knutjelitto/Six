namespace SixComp.ParseTree
{
    public class LetPattern : SyntaxNode, AnyPattern
    {
        public LetPattern(AnyPattern pattern)
        {
            Pattern = pattern;
        }

        public AnyPattern Pattern { get; }

        public static LetPattern Parse(Parser parser)
        {
            parser.Consume(ToKind.KwLet);

            var pattern = AnyPattern.Parse(parser);

            return new LetPattern(pattern);
        }

        public override string ToString()
        {
            return $"let {Pattern}";
        }
    }
}
