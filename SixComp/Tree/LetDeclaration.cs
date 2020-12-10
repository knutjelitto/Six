using SixComp.Support;

namespace SixComp
{
    public partial class ParseTree
    {
        public class LetDeclaration : IDeclaration
        {
            public LetDeclaration(Prefix prefix, PatternInitializerList initializers)
            {
                Prefix = prefix;
                Initializers = initializers;
            }

            public Prefix Prefix { get; }
            public PatternInitializerList Initializers { get; }

            public static LetDeclaration Parse(Parser parser, Prefix prefix)
            {
                // already parsed //parser.Consume(ToKind.KwLet);

                var initializers = PatternInitializerList.Parse(parser);

                return new LetDeclaration(prefix, initializers);
            }

            public void Write(IWriter writer)
            {
                writer.WriteLine($"let {Initializers}");
            }

            public override string ToString()
            {
                return $"let {Initializers}";
            }
        }
    }
}