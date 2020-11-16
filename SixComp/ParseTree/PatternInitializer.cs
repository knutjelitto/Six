using SixComp.Support;

namespace SixComp.ParseTree
{
    public class PatternInitializer : IWriteable
    {
        public PatternInitializer(AnyPattern pattern, Initializer? initializer)
        {
            Pattern = pattern;
            Initializer = initializer;
        }

        public AnyPattern Pattern { get; }
        public Initializer? Initializer { get; }

        public static PatternInitializer Parse(Parser parser)
        {
            var pattern = AnyPattern.Parse(parser);
            var initializer = parser.Try(ToKind.Equal, Initializer.Parse);

            return new PatternInitializer(pattern, initializer);
        }

        public override string ToString()
        {
            return $"{Pattern}{Initializer}";
        }
    }
}
