namespace SixComp.Tree
{
    public class VarPattern : SyntaxNode, AnyPattern
    {
        public VarPattern(AnyPattern pattern)
        {
            Pattern = pattern;
        }

        public AnyPattern Pattern { get; }

        public static VarPattern Parse(Parser parser)
        {
            parser.Consume(ToKind.KwVar);

            var pattern = AnyPattern.Parse(parser);

            return new VarPattern(pattern);
        }

        public override string ToString()
        {
            return $"var {Pattern}";
        }
    }
}
