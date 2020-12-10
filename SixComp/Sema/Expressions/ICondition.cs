using SixComp.Support;

namespace SixComp.Sema
{
    public interface ICondition : IExpression
    {
        public static IExpression Build(IScoped outer, ParseTree.ICondition tree)
        {
            return Visit(outer, (dynamic)tree);
        }

        private static IExpression Visit(IScoped outer, ParseTree.PatternCondition.LetPatternCondition tree)
        {
            return new LetPatternCondition(outer, tree);
        }

        private static IExpression Visit(IScoped outer, ParseTree.PatternCondition.VarPatternCondition tree)
        {
            return new VarPatternCondition(outer, tree);
        }

        private static IExpression Visit(IScoped outer, ParseTree.PatternCondition.CasePatternCondition tree)
        {
            return new CasePatternCondition(outer, tree);
        }

        private static IExpression Visit(IScoped outer, ParseTree.ExpressionCondition tree)
        {
            return Build(outer, tree.Expression);
        }

        private static IExpression Visit(IScoped outer, ParseTree.AvailableCondition tree)
        {
            return new AvailableCondition(outer, tree);
        }

        private static IExpression Visit(IScoped outer, ParseTree.ICondition tree)
        {
            return new Dummy(outer, tree);
        }

        public class AvailableCondition : Base<ParseTree.AvailableCondition>, ICondition
        {
            public AvailableCondition(IScoped outer, ParseTree.AvailableCondition tree)
                : base(outer, tree)
            {
            }

            public override void Report(IWriter writer)
            {
                Tree.Tokens.ToString().Report(writer, Strings.Head.Available);
            }
        }

        public abstract class PatternCondition : Base<ParseTree.PatternCondition>, ICondition
        {
            protected PatternCondition(IScoped outer, ParseTree.PatternCondition tree)
                : base(outer, tree)
            {
                Pattern = IPattern.Build(outer, tree.Pattern);
                Type = ITypeDefinition.MaybeBuild(outer, tree.Type);
                Initializer = IExpression.Build(outer, tree.Initializer);
            }

            public IPattern Pattern { get; }
            public ITypeDefinition? Type { get; }
            public IExpression Initializer { get; }

            protected void Report(IWriter writer, string label)
            {
                using (writer.Indent(label))
                {
                    Pattern.Report(writer);
                    Type?.Report(writer);
                    Initializer.Report(writer, Strings.Head.Initializer);
                }
            }
        }

        public class LetPatternCondition : PatternCondition
        {
            public LetPatternCondition(IScoped outer, ParseTree.PatternCondition tree)
                : base(outer, tree)
            {
            }

            public override void Report(IWriter writer)
            {
                Report(writer, Strings.Head.Let);
            }
        }

        public class VarPatternCondition : PatternCondition
        {
            public VarPatternCondition(IScoped outer, ParseTree.PatternCondition tree)
                : base(outer, tree)
            {
            }

            public override void Report(IWriter writer)
            {
                Report(writer, Strings.Head.Var);
            }
        }

        public class CasePatternCondition : PatternCondition
        {
            public CasePatternCondition(IScoped outer, ParseTree.PatternCondition tree)
                : base(outer, tree)
            {
            }

            public override void Report(IWriter writer)
            {
                Report(writer, Strings.Head.Case);
            }
        }
    }
}
