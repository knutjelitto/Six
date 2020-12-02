using SixComp.Support;

namespace SixComp.Sema
{
    public class PostfixSelf : Expression<Tree.PostfixSelfExpression>
    {
        public PostfixSelf(IScoped outer, Tree.PostfixSelfExpression tree)
            : base(outer, tree)
        {
            Left = IExpression.Build(Outer, Tree.Left);
        }
        public IExpression Left { get; }

        public override void Report(IWriter writer)
        {
            writer.WriteLine("postfix .self");
            writer.Indent(() => Left.Report(writer));
        }
    }
}
