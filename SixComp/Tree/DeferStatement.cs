using SixComp.Support;

namespace SixComp
{
    public partial class ParseTree
    {
        public class DeferStatement : IStatement
        {
            public DeferStatement(CodeBlock block)
            {
                Block = block;
            }

            public CodeBlock Block { get; }

            public static DeferStatement Parse(Parser parser)
            {
                parser.Consume(ToKind.KwDefer);

                var block = CodeBlock.Parse(parser);

                return new DeferStatement(block);
            }

            public void Write(IWriter writer)
            {
                writer.WriteLine($"defer{Block}");
            }

            public override string ToString()
            {
                return $"defer{Block}";
            }
        }
    }
}