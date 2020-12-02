using SixComp.Support;

namespace SixComp.Sema
{
    public class PrefixExpression : Expression<Tree.PrefixExpression>, INamed
    {
        public PrefixExpression(IScoped outer, Tree.PrefixExpression tree)
            : base(outer, tree)
        {
            Name = new BaseName(Outer, Tree.Operator);
            Right = IExpression.Build(outer, Tree.Operand);
        }

        public BaseName Name { get; }
        public IExpression Right { get; }

        public override void Report(IWriter writer)
        {
            writer.WriteLine("prefix {Name}");
            writer.Indent(() => Right.Report(writer));
            
        }
    }
}
