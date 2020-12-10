using SixComp.Support;

namespace SixComp.Sema
{
    public class Defer : Base<ParseTree.DeferStatement>, IStatement
    {
        public Defer(IScoped outer, ParseTree.DeferStatement tree)
            : base(outer, tree)
        {
            Block = new CodeBlock(Outer, tree.Block);
        }

        public CodeBlock Block { get; }

        public override void Report(IWriter writer)
        {
            Tree.Tree(writer);
            Block.Statements.ReportList(writer, Strings.Head.Defer);
        }
    }
}
