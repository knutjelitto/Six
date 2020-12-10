namespace SixComp
{
    public partial class ParseTree
    {
        public class AsPattern : SyntaxNode, IPattern
        {
            public AsPattern(IPattern pattern, IType type)
            {
                Pattern = pattern;
                Type = type;
            }

            public IPattern Pattern { get; }
            public IType Type { get; }

            public static AsPattern Parse(Parser parser, IPattern pattern)
            {
                parser.Consume(ToKind.KwAs);
                var type = IType.Parse(parser);

                return new AsPattern(pattern, type);
            }
            public override string ToString()
            {
                return $"{Pattern} as {Type}";
            }
        }
    }
}