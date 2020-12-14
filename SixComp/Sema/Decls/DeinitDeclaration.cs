using Six.Support;

namespace SixComp.Sema
{
    public class DeinitDeclaration : Base<ParseTree.DeinitializerDeclaration>, IDeclaration
    {
        public DeinitDeclaration(IScoped outer, ParseTree.DeinitializerDeclaration tree)
            : base(outer, tree)
        {
            Block = new CodeBlock(outer, tree.Block);
        }

        public CodeBlock Block { get; }

        public override void Report(IWriter writer)
        {
            Tree.Tree(writer);
            using (writer.Indent(Strings.Head.Deinit))
            {
                Block.Report(writer);
            }
        }
    }
}
