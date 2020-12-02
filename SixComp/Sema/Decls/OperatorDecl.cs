using SixComp.Support;
using System;

namespace SixComp.Sema
{
    public class OperatorDecl : Base<Tree.OperatorDeclaration>, IDeclaration
    {
        public enum OperatorFix
        {
            Prefix,
            Postfix,
            Infix,
        }

        public OperatorDecl(IScoped outer, Tree.OperatorDeclaration tree)
            : base(outer, tree)
        {
            Operator = new BaseName(Outer, tree.Operator);

            Fix = tree.Kind switch
            {
                SixComp.Tree.OperatorDeclaration.OperatorKind.Prefix => OperatorFix.Prefix,
                SixComp.Tree.OperatorDeclaration.OperatorKind.Postfix => OperatorFix.Postfix,
                SixComp.Tree.OperatorDeclaration.OperatorKind.Infix => OperatorFix.Infix,
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        public BaseName Operator { get; }
        public OperatorFix Fix { get; }

        public override void Report(IWriter writer)
        {
            var fix = Fix switch
            {
                OperatorFix.Prefix => "prefix",
                OperatorFix.Postfix => "postfix",
                OperatorFix.Infix => "infix",
                _ => throw new ArgumentOutOfRangeException(),
            };
            writer.WriteLine($"{fix} {Strings.Operator} {Operator}");
        }
    }
}
