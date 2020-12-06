using SixComp.Support;

namespace SixComp.Sema
{
    public class PrefixExpression : Base<Tree.PrefixExpression>, IExpression, INamed
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
            using (writer.Indent(Strings.Head.Prefix))
            {
                Name.Report(writer, Strings.Head.Operator);
                Right.Report(writer, Strings.Head.Right);
            }            
        }
    }
}
