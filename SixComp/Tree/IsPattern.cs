namespace SixComp
{
    public partial class ParseTree
    {
        public class IsPattern : SyntaxNode, IPattern
        {
            public IsPattern(IType type)
            {
                Type = type;
            }

            public IType Type { get; }

            public static IsPattern Parse(Parser parser)
            {
                parser.Consume(ToKind.KwIs);
                var type = IType.Parse(parser);

                return new IsPattern(type);
            }
        }
    }
}