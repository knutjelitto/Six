using SixComp.Support;

namespace SixComp.Sema
{
    public class InOutExpression : Base<ParseTree.InOutExpression>, IExpression
    {
        public InOutExpression(IScoped outer, ParseTree.InOutExpression tree)
            : base(outer, tree)
        {
            Expression = IExpression.Build(Outer, Tree.Expression);
        }

        public IExpression Expression { get; }

        public override void Report(IWriter writer)
        {
            Expression.Report(writer, Strings.Head.InOut);
        }
    }
}
