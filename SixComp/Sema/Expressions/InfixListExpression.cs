using SixComp.Support;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SixComp.Sema
{
    public class InfixList : Base<Tree.InfixList>, IExpression
    {
        public InfixList(IScoped outer, Tree.InfixList tree)
            : base(outer, tree)
        {
            Debug.Assert(tree.Binaries.Count > 0);

            Infix = null;
        }

        public IExpression? Infix { get; private set; }

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

        public class InfixParts : Items<InfixPart, Tree.BinaryExpressionList>
        {
            public InfixParts(IScoped outer, Tree.BinaryExpressionList tree)
                : base(outer, tree, Enum(outer, tree))
            {
            }

            public override void Report(IWriter writer)
            {
                foreach (var part in this)
                {
                    part.Report(writer);
                }
            }

            private static IEnumerable<InfixPart> Enum(IScoped outer, Tree.BinaryExpressionList tree)
            {
                return tree.Select(part => new InfixPart(outer, part));
            }
        }

        public class InfixPart : Base<Tree.BinaryExpression>
        {
            public InfixPart(IScoped outer, Tree.BinaryExpression tree) : base(outer, tree)
            {
                Operator = tree.Operator;
                Right = IExpression.Build(outer, tree.Right);
            }

            public Tree.Operator Operator { get; }
            public IExpression Right { get; }

            public override void Report(IWriter writer)
            {
                writer.WriteLine($"{Operator}");
                Right.Report(writer, Strings.Head.Right);
            }
        }
    }
}
