using SixComp.Support;

namespace SixComp.Sema
{
    public class Break : Statement<Tree.BreakStatement>
    {
        public Break(IScoped outer, Tree.BreakStatement tree)
            : base(outer, tree)
        {
            Label = Tree.Label == null ? null : new BaseName(Outer, Tree.Label);
        }

        public BaseName? Label { get; }

        public override void Report(IWriter writer)
        {
            writer.Write(Strings.Break);
            writer.Indent(() => Label?.Report(writer));
        }
    }
}
