using SixComp.Support;

namespace SixComp.ParseTree
{
    public class PatternCondition : AnyCondition
    {
        public static readonly TokenSet Firsts = new TokenSet(ToKind.KwCase, ToKind.KwLet, ToKind.KwVar);

        public PatternCondition(AnyPattern pattern, Initializer init)
        {
            Pattern = pattern;
            Init = init;
        }

        public AnyPattern Pattern { get; }
        public Initializer Init { get; }

        public static PatternCondition Parse(Parser parser)
        {
            parser.Consume(Firsts);

            var pattern = AnyPattern.Parse(parser);
            var init = Initializer.Parse(parser);

            return new PatternCondition(pattern, init);
        }
    }
}
