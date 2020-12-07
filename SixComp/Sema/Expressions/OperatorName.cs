using SixComp.Support;

namespace SixComp.Sema
{
    public class OperatorName : Base<Tree.OperatorExpression>, IExpression, INamed
    {
        public OperatorName(IScoped outer, Tree.OperatorExpression tree)
            : base(outer, tree)
        {
            Name = new BaseName(outer, tree.Operator);
        }

        public BaseName Name { get; }

        public override void Resolve(IWriter writer)
        {
            // TODO: TODO
            //Resolve(writer, Name);
        }

        public override void Report(IWriter writer)
        {
            Name.Report(writer, Strings.Head.Operator);
        }
    }
}
