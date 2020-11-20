using SixComp.Support;

namespace SixComp.ParseTree
{
    public class PatternVarDeclaration : AnyVarDeclaration
    {
        private PatternVarDeclaration(Prefix prefix, PatternInitializerList initializers)
        {
            Prefix = prefix;
            Initializers = initializers;
        }

        public Prefix Prefix { get; }
        public PatternInitializerList Initializers { get; }

        public static PatternVarDeclaration Parse(Parser parser, Prefix prefix)
        {
            // `var` already gone

            var initializers = PatternInitializerList.Parse(parser);

            return new PatternVarDeclaration(prefix, initializers);
        }

        public void Write(IWriter writer)
        {
            writer.WriteLine($"var {Prefix}{Initializers}");
        }
    }
}
