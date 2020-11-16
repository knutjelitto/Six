using SixComp.Support;

namespace SixComp.ParseTree
{
    public class LetDeclaration : AnyDeclaration
    {
        public LetDeclaration(PatternInitializerList initializers)
        {
            Initializers = initializers;
        }

        public PatternInitializerList Initializers { get; }

        public static LetDeclaration Parse(Parser parser)
        {
            parser.Consume(ToKind.KwLet);

            var initializers = PatternInitializerList.Parse(parser);

            return new LetDeclaration(initializers);
        }

        public void Write(IWriter writer)
        {
            writer.WriteLine($"let {Initializers}");
        }
    }
}
