using Six.Support;

namespace SixComp.Sema
{
    public class YieldStatement : Base<ParseTree.YieldStatement>, IStatement
    {
        public YieldStatement(IScoped outer, ParseTree.YieldStatement tree)
            : base(outer, tree)
        {
            Value = IExpression.MaybeBuild(Outer, tree.Value);
        }

        public IExpression? Value { get; }

        public override void Report(IWriter writer)
        {
            Value.Report(writer, Strings.Head.Yield, true);
        }
    }
}
