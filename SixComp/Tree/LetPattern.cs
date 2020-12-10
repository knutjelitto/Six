namespace SixComp
{
    public partial class ParseTree
    {
        public class LetPattern : SyntaxNode, IPattern
        {
            public LetPattern(IPattern pattern)
            {
                Pattern = pattern;
            }

            public IPattern Pattern { get; }

            public static LetPattern Parse(Parser parser)
            {
                parser.Consume(ToKind.KwLet);

                var pattern = IPattern.Parse(parser);

                return new LetPattern(pattern);
            }

            public override string ToString()
            {
                return $"let {Pattern}";
            }
        }
    }
}