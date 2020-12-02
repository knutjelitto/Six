using SixComp.Support;

namespace SixComp.Sema
{
    public interface ICondition : IExpression
    {
        public static IExpression Build(IScoped outer, Tree.AnyCondition tree)
        {
            return Visit(outer, (dynamic)tree);
        }

        private static IExpression Visit(IScoped outer, Tree.PatternCondition.LetPatternCondition tree)
        {
            return new LetPatternCondition(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.PatternCondition.VarPatternCondition tree)
        {
            return new VarPatternCondition(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.PatternCondition.CasePatternCondition tree)
        {
            return new CasePatternCondition(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.ExpressionCondition tree)
        {
            return Build(outer, tree.Expression);
        }

        private static IExpression Visit(IScoped outer, Tree.AnyCondition tree)
        {
            return new Dummy(outer, tree);
        }

        public class PatternCondition : Base<Tree.PatternCondition>, ICondition
        {
            public PatternCondition(IScoped outer, Tree.PatternCondition tree)
                : base(outer, tree)
            {
                Pattern = IPattern.Build(outer, tree.Pattern);
                Type = IType.MaybeBuild(outer, tree.Type);
                Initializer = IExpression.Build(outer, tree.Initializer);
            }

            public IPattern Pattern { get; }
            public IType? Type { get; }
            public IExpression Initializer { get; }

            public override void Report(IWriter writer)
            {
                using (writer.Indent())
                {
                    Pattern.Report(writer);
                    Type?.Report(writer);
                    Initializer.Report(writer);
                }
            }
        }

        public class LetPatternCondition : PatternCondition
        {
            public LetPatternCondition(IScoped outer, Tree.PatternCondition tree)
                : base(outer, tree)
            {
            }

            public override void Report(IWriter writer)
            {
                writer.WriteLine(Strings.LetPatternCondition);
                base.Report(writer);
            }
        }

        public class VarPatternCondition : PatternCondition
        {
            public VarPatternCondition(IScoped outer, Tree.PatternCondition tree)
                : base(outer, tree)
            {
            }

            public override void Report(IWriter writer)
            {
                writer.WriteLine(Strings.VarPatternCondition);
                base.Report(writer);
            }
        }

        public class CasePatternCondition : PatternCondition
        {
            public CasePatternCondition(IScoped outer, Tree.PatternCondition tree)
                : base(outer, tree)
            {
            }

            public override void Report(IWriter writer)
            {
                writer.WriteLine(Strings.CasePatternCondition);
                writer.WriteLine($";; {Tree}");
                base.Report(writer);
            }
        }
    }
}
