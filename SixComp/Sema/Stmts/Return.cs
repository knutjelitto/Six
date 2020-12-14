using Six.Support;

namespace SixComp.Sema
{
    public class Return : Base<ParseTree.ReturnStatement>, IStatement
    {
        public Return(IScoped outer, ParseTree.ReturnStatement tree)
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
