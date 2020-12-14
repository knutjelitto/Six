using Six.Support;

namespace SixComp
{
    public partial class ParseTree
    {
        public class PrecGroupDeclaration : IDeclaration
        {
            public PrecGroupDeclaration(Prefix prefix, BaseName name, PrecGroupAttributeList attributes)
            {
                Prefix = prefix;
                Name = name;
                Attributes = attributes;
            }

            public Prefix Prefix { get; }
            public BaseName Name { get; }
            public PrecGroupAttributeList Attributes { get; }

            public static PrecGroupDeclaration Parse(Parser parser, Prefix prefix)
            {
                parser.Consume(Contextual.Precedencegroup);
                var name = BaseName.Parse(parser);
                parser.Consume(ToKind.LBrace);
                var attributes = PrecGroupAttributeList.Parse(parser);
                parser.Consume(ToKind.RBrace);

                return new PrecGroupDeclaration(prefix, name, attributes);
            }

            public void Write(IWriter writer)
            {
                Prefix.Write(writer);
                writer.WriteLine($"{Contextual.Precedencegroup} {Name}");
                using (writer.Block())
                {
                    Attributes.Write(writer);
                }
            }
        }
    }
}
