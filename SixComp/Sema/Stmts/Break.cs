using Six.Support;

namespace SixComp.Sema
{
    public class Break : Base<ParseTree.BreakStatement>, IStatement
    {
        public Break(IScoped outer, ParseTree.BreakStatement tree)
            : base(outer, tree)
        {
            Label = Tree.Label == null ? null : new BaseName(Outer, Tree.Label);
        }

        public BaseName? Label { get; }

        public override void Report(IWriter writer)
        {
            Label.Report(writer, Strings.Head.Break, true);
        }
    }
}
