namespace SixComp
{
    public partial class ParseTree
    {
        public class VarPattern : SyntaxNode, IPattern
        {
            public VarPattern(IPattern pattern)
            {
                Pattern = pattern;
            }

            public IPattern Pattern { get; }

            public static VarPattern Parse(Parser parser)
            {
                parser.Consume(ToKind.KwVar);

                var pattern = IPattern.Parse(parser);

                return new VarPattern(pattern);
            }

            public override string ToString()
            {
                return $"var {Pattern}";
            }
        }
    }
}