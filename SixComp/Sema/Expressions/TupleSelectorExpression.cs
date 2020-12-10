using SixComp.Support;

namespace SixComp.Sema
{
    public class TupleSelectorExpression: Base<ParseTree.ExplicitMemberExpression.TupleMemberSelector>, IExpression
    {
        public TupleSelectorExpression(IScoped outer, ParseTree.ExplicitMemberExpression.TupleMemberSelector tree)
            : base(outer, tree)
        {
            Left = IExpression.Build(Outer, Tree.Left);
            Operator = new BaseName(Outer, tree.Operator);
            Number = IExpression.Build(outer, Tree.Number);
        }

        public IExpression Left { get; }
        public BaseName Operator { get; }
        public IExpression Number { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Select))
            {
                Left.Report(writer, Strings.Head.Left);
                Operator.Report(writer, Strings.Head.Operator);
                Number.Report(writer, Strings.Head.Number);
            }
        }
    }
}
