using SixComp.Support;

namespace SixComp.Sema
{
    public class SubscriptExpression : Base<Tree.SubscriptExpression>, IExpression
    {
        public SubscriptExpression(IScoped outer, Tree.SubscriptExpression tree)
            : base(outer, tree)
        {
            Left = IExpression.Build(Outer, Tree.Left);
            Arguments = new FunctionArguments(Outer, Tree.Subscript.Arguments);
        }

        public IExpression Left { get; }
        public FunctionArguments Arguments { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Subscript))
            {
                Left.Report(writer, Strings.Head.Left);
                Arguments.Report(writer);
            }
        }
    }
}
