using SixComp.Support;

namespace SixComp
{
    public partial class ParseTree
    {
        public class PatternInitializer : IWritable
        {
            public PatternInitializer(IPattern pattern, TypeAnnotation? type, Initializer? initializer)
            {
                Pattern = pattern;
                Type = type;
                Initializer = initializer;
            }

            public IPattern Pattern { get; }
            public TypeAnnotation? Type { get; }
            public Initializer? Initializer { get; }

            public static PatternInitializer Parse(Parser parser)
            {
                var pattern = IPattern.Parse(parser);
                var type = parser.Try(TypeAnnotation.Firsts, TypeAnnotation.Parse);
                var initializer = parser.Try(ToKind.Assign, Initializer.Parse);

                return new PatternInitializer(pattern, type, initializer);
            }

            public override string ToString()
            {
                return $"{Pattern}{Type}{Initializer}";
            }
        }
    }
}