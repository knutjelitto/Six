using SixComp.Support;

namespace SixComp.Sema
{
    public class Return : Statement<Tree.ReturnStatement>
    {
        public Return(IScoped outer, Tree.ReturnStatement tree)
            : base(outer, tree)
        {
            Value = IExpression.MaybeBuild(Outer, tree.Value);
        }

        public IExpression? Value { get; }

        public override void Report(IWriter writer)
        {
            writer.WriteLine("return");
            writer.Indent(() => Value?.Report(writer));
        }
    }
}
