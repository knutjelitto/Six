using SixComp.Support;

namespace SixComp.Sema
{
    public class FunctionArgument : Base<Tree.Argument>
    {
        public FunctionArgument(IScoped outer, Tree.Argument tree)
            : base(outer, tree)
        {
            Label = tree.Label == null ? null : new BaseName(Outer, tree.Label.Name);
            Value = IExpression.Build(Outer, tree.Value);
        }

        public BaseName? Label { get; }
        public IExpression Value { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Argument))
            {
                Label?.Report(writer, Strings.Head.Label);
                Value.Report(writer, Strings.Head.Value);
            }
        }
    }
}
