using SixComp.Support;

namespace SixComp.Sema
{
    public class LiteralExpression : Base<ParseTree.ILiteralExpression>, IExpression
    {
        public LiteralExpression(IScoped outer, ParseTree.ILiteralExpression tree)
            : base(outer, tree)
        {
            Text = tree.ToString()!;
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
