using SixComp.Support;

namespace SixComp.Sema
{
    public class Postfix : Expression<Tree.PostfixOpExpression>
    {
        public Postfix(IScoped outer, Tree.PostfixOpExpression tree)
            : base(outer, tree)
        {
            Left = IExpression.Build(Outer, Tree.Left);
            Operator = new BaseName(Outer, Tree.Operator);
        }
        public IExpression Left { get; }
        public BaseName Operator { get; }

        public override void Report(IWriter writer)
        {
            writer.WriteLine($"{Strings.Postfix} `{Operator}`");
            writer.Indent(() => Left.Report(writer));
        }
    }
}
