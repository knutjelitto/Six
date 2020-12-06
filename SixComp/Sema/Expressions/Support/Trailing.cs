using SixComp.Support;

namespace SixComp.Sema
{
    public class Trailing : Base<Tree.TrailingClosure>
    {
        public Trailing(IScoped outer, Tree.TrailingClosure tree)
            : base(outer, tree)
        {
            Label = BaseName.Maybe(Outer, tree.Label?.Name);
            Closure = new ClosureExpression(Outer, tree.Closure);
        }

        public BaseName? Label { get; }
        public ClosureExpression Closure { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Trailing))
            {
                Label.Report(writer, Strings.Head.Label);
                Closure.Report(writer);
            }
        }
    }
}
