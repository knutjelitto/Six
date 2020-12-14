using Six.Support;

namespace SixComp.Sema
{
    public class ConditionalExpression : Base, IExpression
    {
        public ConditionalExpression(IScoped outer, IOperator @operator, IExpression condition, IExpression ifTrue, IExpression isFalse)
            : base(outer)
        {
            Operator = @operator;
            Condition = condition;
            IfTrue = ifTrue;
            IfFalse = isFalse;
        }

        public IOperator Operator { get; }
        public IExpression Condition { get; }
        public IExpression IfTrue { get; }
        public IExpression IfFalse { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Conditional))
            {
                Condition.Report(writer, Strings.Head.Condition);
                IfTrue.Report(writer, Strings.Head.IfTrue);
                IfFalse.Report(writer, Strings.Head.IfFalse);
            }
        }
    }
}
