using SixComp.Support;

namespace SixComp.Sema
{
    public class LiteralExpression : Expression<Tree.AnyLiteralExpression>
    {
        public LiteralExpression(IScoped outer, Tree.AnyLiteralExpression tree)
            : base(outer, tree)
        {
            Text = tree.ToString();
        }

        public string Text { get; }

        public override void Report(IWriter writer)
        {
            this.Report(writer, Strings.Head.Literal);
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
