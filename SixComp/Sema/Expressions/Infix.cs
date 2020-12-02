using SixComp.Support;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SixComp.Sema
{
    public class Infix : Expression<Tree.Expression>
    {
        public Infix(IScoped outer, Tree.Expression tree)
            : base(outer, tree)
        {
            Debug.Assert(tree.Binaries.Count > 0);

            Left = IExpression.Build(outer, tree.Left);
            Rest = new InfixParts(outer, tree.Binaries);
        }

        public IExpression Left { get; }
        public InfixParts Rest { get; }
        

        public override void Report(IWriter writer)
        {
            Tree.Tree(writer);
            using (writer.Indent(Strings.Head.Infix))
            {
                writer.WriteLine(Strings.Incomplete);
                Left.Report(writer, Strings.Head.Left);
                Rest.Report(writer);
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
