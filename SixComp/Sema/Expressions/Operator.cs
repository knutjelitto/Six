using SixComp.Support;

namespace SixComp.Sema
{
    public class Operator : Base<Tree.OperatorExpression>, IExpression, INamed
    {
        public Operator(IScoped outer, Tree.OperatorExpression tree)
            : base(outer, tree)
        {
            Name = new BaseName(outer, tree.Operator);
        }

        public BaseName Name { get; }

        public override void Report(IWriter writer)
        {
            Name.Report(writer, Strings.OperatorExpression);
        }
    }
}
