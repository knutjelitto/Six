using SixComp.Support;

namespace SixComp.Sema
{
    public class FuncCall : Expression<Tree.FunctionCallExpression>
    {
        public FuncCall(IScoped outer, Tree.FunctionCallExpression tree)
            : base(outer, tree)
        {
            Left = IExpression.Build(outer, Tree.Left);
            Arguments = new FuncArguments(outer, tree.Arguments.Arguments);
        }

        public IExpression Left { get; }
        public FuncArguments Arguments { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Call))
            {
                Left.Report(writer);
                Arguments.Report(writer);
            }
        }
    }
}
