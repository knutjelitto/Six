using SixComp.Support;

namespace SixComp.Sema
{
    public class YieldStatement : Base<Tree.YieldStatement>, IStatement
    {
        public YieldStatement(IScoped outer, Tree.YieldStatement tree)
            : base(outer, tree)
        {
            Value = IExpression.MaybeBuild(Outer, tree.Value);
        }

        public IExpression? Value { get; }

        public override void Resolve(IWriter writer)
        {
            Resolve(writer, Value);
        }

        public override void Report(IWriter writer)
        {
            Value.Report(writer, Strings.Head.Yield, true);
        }
    }
}
