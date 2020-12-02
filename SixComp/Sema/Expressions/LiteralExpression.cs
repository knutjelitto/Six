using SixComp.Support;

namespace SixComp.Sema
{
    public class LiteralExpression : Expression<Tree.AnyLiteralExpression>
    {
        public LiteralExpression(IScoped outer, Tree.AnyLiteralExpression tree)
            : base(outer, tree)
        {
        }

        public override void Report(IWriter writer)
        {
            writer.WriteLine($"{Tree}");
        }
    }
}
