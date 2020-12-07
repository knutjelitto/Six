using SixComp.Support;

namespace SixComp.Sema
{
    public class InOutExpression : Base<Tree.InOutExpression>, IExpression
    {
        public InOutExpression(IScoped outer, Tree.InOutExpression tree)
            : base(outer, tree)
        {
            Expression = IExpression.Build(Outer, Tree.Expression);
        }

        public IExpression Expression { get; }

        public override void Resolve(IWriter writer)
        {
            Resolve(writer, Expression);
        }

        public override void Report(IWriter writer)
        {
            Expression.Report(writer, Strings.Head.InOut);
        }
    }
}
