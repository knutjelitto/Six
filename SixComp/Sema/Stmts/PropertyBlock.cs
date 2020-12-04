using SixComp.Common;
using SixComp.Support;

namespace SixComp.Sema

{
    public class PropertyBlock : Base<Tree.PropertyBlock>
    {
        public PropertyBlock(IScoped outer, Tree.PropertyBlock tree)
            : base(outer, tree)
        {
            Kind = tree.Kind;
            BlockName = new BaseName(Outer, tree.BlockName);
            SetterName = BaseName.Maybe(Outer, tree.SetterName?.Name);
            Block = (CodeBlock?)IStatement.MaybeBuild(Outer, tree.Block);
        }

        public BlockKind Kind { get; }
        public BaseName BlockName { get; }
        public BaseName? SetterName { get; }
        public CodeBlock? Block { get; }

        public override void Report(IWriter writer)
        {
            Tree.Tree(writer);
            Kind.Report(writer, Strings.Head.Kind);
            SetterName.Report(writer, Strings.Head.SetterName);
            Block?.Statements.ReportList(writer, BlockName.Text + ":");
        }
    }
}
