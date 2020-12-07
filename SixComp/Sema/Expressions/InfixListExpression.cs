using SixComp.Support;
using System.Diagnostics;
using System.Linq;

namespace SixComp.Sema
{
    public class InfixListExpression : Base<Tree.InfixList>, IExpression
    {
        public InfixListExpression(IScoped outer, Tree.InfixList tree)
            : base(outer, tree)
        {
            Debug.Assert(tree.Binaries.Count > 0);

            Infix = null;
        }

        public IExpression? Infix { get; private set; }

        public override void Resolve(IWriter writer)
        {
            Resolve(writer, Infix);
        }

        public override void Report(IWriter writer)
        {
            Tree.Tree(writer);
            if (Infix == null)
            {
                using (writer.Indent(Strings.Head.InfixList))
                {
                    writer.WriteLine(Strings.Incomplete);
                }
            }
            else
            {
                Infix.Report(writer);
            }
        }

        public void MakeInfix()
        {
            var operators = Tree.Binaries.Select(bin => IOperator.Build(Outer, bin.Operator)).ToList();
            var rights = Tree.Binaries.Select(bin => IExpression.Build(Outer, bin.Right)).ToList();

            var offset = 0;
            Infix = Build(IExpression.Build(Outer, Tree.Left), null);

            IExpression Build(IExpression left, PrecedenceGroupDeclaration? prec)
            {
                while (offset < operators.Count && operators[offset].GreaterThanOrEqual(prec))
                {
                    var op = operators[offset];
                    var right = rights[offset];
                    offset += 1;
                    while (offset < operators.Count && operators[offset].GreaterThan(op.Precedence))
                    {
                        right = Build(right, operators[offset].Precedence);
                        offset += 1;
                    }
                    left = MakeOperation(op, left, right);
                }
                return left;
            }

            IExpression MakeOperation(IOperator op, IExpression left, IExpression right)
            {
                if (op is IOperator.Conditional cond)
                {
                    var mid = IExpression.Build(Outer, cond.Tree.Middle);
                    return new ConditionalExpression(Outer, op, left, mid, right);
                }
                return new InfixExpression(Outer, op, left, right);
            }
        }
    }
}
