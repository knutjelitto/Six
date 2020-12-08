using SixComp.Support;

namespace SixComp.Sema
{
    public class Return : Base<Tree.ReturnStatement>, IStatement
    {
        public Return(IScoped outer, Tree.ReturnStatement tree)
            : base(outer, tree)
        {
            Value = IExpression.MaybeBuild(Outer, tree.Value);
        }

        public IExpression? Value { get; }

        public override void Report(IWriter writer)
        {
            Value.Report(writer, Strings.Head.Return, true);
        }
    }
}
