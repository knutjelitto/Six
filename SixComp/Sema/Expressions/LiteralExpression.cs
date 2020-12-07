using SixComp.Support;

namespace SixComp.Sema
{
    public class LiteralExpression : Base<Tree.AnyLiteralExpression>, IExpression
    {
        public LiteralExpression(IScoped outer, Tree.AnyLiteralExpression tree)
            : base(outer, tree)
        {
            Text = tree.ToString();
        }

        public string Text { get; }

        public override void Resolve(IWriter writer)
        {
            // TODO: TODO
            //UnResolve(writer);
        }

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
