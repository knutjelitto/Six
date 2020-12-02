using SixComp.Support;

namespace SixComp.Tree
{
    public class DeinitializerDeclaration : AnyDeclaration
    {
        public DeinitializerDeclaration(Prefix prefix, CodeBlock block)
        {
            Prefix = prefix;
            Block = block;
        }

        public Prefix Prefix { get; }
        public CodeBlock Block { get; }

        public static DeinitializerDeclaration Parse(Parser parser, Prefix prefix)
        {
            // already parsed //parser.Consume(ToKind.KwDeinit);

            var block = CodeBlock.Parse(parser);

            return new DeinitializerDeclaration(prefix, block);
        }

        public void Write(IWriter writer)
        {
            writer.WriteLine($"deinit");
            Block.Write(writer);
        }
    }
}
