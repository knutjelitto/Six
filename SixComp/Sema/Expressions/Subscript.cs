using SixComp.Support;
using SixComp.Tree;

namespace SixComp.Sema
{
    public class Subscript : Base<Tree.SubscriptExpression>, IExpression
    {
        public Subscript(IScoped outer, SubscriptExpression tree)
            : base(outer, tree)
        {
            Left = IExpression.Build(Outer, Tree.Left);
            Arguments = new FuncArguments(Outer, Tree.Subscript.Arguments);
        }

        public IExpression Left { get; }
        public FuncArguments Arguments { get; }

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
