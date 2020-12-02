using SixComp.Support;

namespace SixComp.Sema
{
    public class TupleSelector: Expression<Tree.ExplicitMemberExpression.TupleMemberSelector>
    {
        public TupleSelector(IScoped outer, Tree.ExplicitMemberExpression.TupleMemberSelector tree)
            : base(outer, tree)
        {
            Left = IExpression.Build(Outer, Tree.Left);
            Number = IExpression.Build(outer, Tree.Number);
        }

        public IExpression Left { get; }
        public IExpression Number { get; }
        public override void Report(IWriter writer)
        {
            writer.WriteLine("select x.n");
            using (writer.Indent())
            {
                Left.Report(writer);
                Number.Report(writer);
            }
        }
    }
}
