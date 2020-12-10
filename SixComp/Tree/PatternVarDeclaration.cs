using SixComp.Support;

namespace SixComp
{
    public partial class ParseTree
    {
        public class PatternVarDeclaration : IVarDeclaration
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

            public static PatternVarDeclaration From(Prefix prefix, PatternInitializerList pattInits)
            {
                return new PatternVarDeclaration(prefix, pattInits);
            }

            public void Write(IWriter writer)
            {
                Prefix.Write(writer);
                writer.WriteLine($"{Initializers}");
            }

            public override string ToString()
            {
                return $"{Prefix}{Initializers}";
            }
        }
    }
}