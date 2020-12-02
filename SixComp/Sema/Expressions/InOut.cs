using SixComp.Support;

namespace SixComp.Sema
{
    public class InOut : Base<Tree.InOutExpression>, IExpression
    {
        public InOut(IScoped outer, Tree.InOutExpression tree)
            : base(outer, tree)
        {
            Expression = IExpression.Build(Outer, Tree.Expression);
        }

        public IExpression Expression { get; }

        public override void Report(IWriter writer)
        {
            writer.WriteLine(Strings.InOut);
            writer.Indent(() => Expression.Report(writer));
        }
    }
}
