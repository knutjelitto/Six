using SixComp.Support;

namespace SixComp.Sema
{
    public class Break : Base<Tree.BreakStatement>, IStatement
    {
        public Break(IScoped outer, Tree.BreakStatement tree)
            : base(outer, tree)
        {
            Label = Tree.Label == null ? null : new BaseName(Outer, Tree.Label);
        }

        public BaseName? Label { get; }

        public override void Resolve(IWriter writer)
        {
            // TODO: labels??
            Resolve(writer, Label);
        }

        public override void Report(IWriter writer)
        {
            Label.Report(writer, Strings.Head.Break, true);
        }
    }
}
