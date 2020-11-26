using SixComp.Support;

namespace SixComp.ParseTree
{
    public class PatternCondition : BaseExpression, AnyCondition
    {
        public static readonly TokenSet Firsts = new TokenSet(ToKind.KwCase, ToKind.KwLet, ToKind.KwVar);

        public PatternCondition(AnyPattern pattern, Initializer initializer)
        {
            Pattern = pattern;
            Initializer = initializer;
        }

        public AnyPattern Pattern { get; }
        public Initializer Initializer { get; }

        public override AnyExpression? LastExpression => Initializer.LastExpression;

        public static PatternCondition Parse(Parser parser)
        {
            parser.Consume(Firsts);

            var pattern = AnyPattern.Parse(parser);
            var type = parser.Try(TypeAnnotation.Firsts, TypeAnnotation.Parse);
            var init = Initializer.Parse(parser);

            return new PatternCondition(pattern, init);
        }

        public override string ToString()
        {
            return $"{Pattern}{Initializer}";
        }
    }
}
