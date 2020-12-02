using SixComp.Support;

namespace SixComp.Sema
{
    public class ExpressionList : Expression<Tree.Expression>
    {
        public ExpressionList(IScoped outer, Tree.Expression tree)
            : base(outer, tree)
        {
            Left = IExpression.Build(outer, tree.Left);
        }

        IExpression Left { get; }

        public override void Report(IWriter writer)
        {
            writer.WriteLine(Strings.ExpressionList);
            using (writer.Indent())
            {
                Left.Report(writer);
                writer.WriteLine($";; REST {Strings.Missing}{Tree.Binaries}");
            }
        }
    }
}
