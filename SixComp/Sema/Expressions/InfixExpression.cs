using SixComp.Support;

namespace SixComp.Sema
{
    public class InfixExpression : Base, IExpression
    {
        public InfixExpression(IScoped outer, IOperator @operator, IExpression left, IExpression right)
            : base(outer)
        {
            Operator = @operator;
            Left = left;
            Right = right;
        }

        public IOperator Operator { get; }
        public IExpression Left { get; }
        public IExpression Right { get; }

        public override void Resolve(IWriter writer)
        {
            // TODO: find operator function
            Resolve(writer, Left, Right);
        }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Infix))
            {
                Operator.Report(writer, Strings.Head.Operator.ToString());
                Left.Report(writer, Strings.Head.Left);
                Right.Report(writer, Strings.Head.Right);
            }
        }
    }
}
