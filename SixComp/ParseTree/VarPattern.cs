namespace SixComp.ParseTree
{
    public class VarPattern : AnyPattern
    {
        public VarPattern(AnyPattern pattern)
        {
            Pattern = pattern;
        }

        public AnyPattern Pattern { get; }

        public static LetPattern Parse(Parser parser)
        {
            parser.Consume(ToKind.KwVar);

            var pattern = AnyPattern.Parse(parser);

            return new LetPattern(pattern);
        }
    }
}
