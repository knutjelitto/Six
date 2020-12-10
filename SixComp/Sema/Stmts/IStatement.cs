namespace SixComp.Sema
{
    public interface IStatement : IScoped, IReportable
    {
        public static IStatement? MaybeBuild(IScoped scope, ParseTree.IStatement? tree)
        {
            if (tree == null)
            {
                return null;
            }
            return Visit(scope, (dynamic)tree);
        }

        public static IStatement Build(IScoped outer, ParseTree.IStatement tree)
        {
            return Visit(outer, (dynamic)tree);
        }

        private static IStatement Visit(IScoped outer, ParseTree.CodeBlock tree)
        {
            return new CodeBlock(outer, tree);
        }

        private static IStatement Visit(IScoped outer, ParseTree.ReturnStatement tree)
        {
            return new Return(outer, tree);
        }

        private static IStatement Visit(IScoped outer, ParseTree.DeferStatement tree)
        {
            return new Defer(outer, tree);
        }

        private static IStatement Visit(IScoped outer, ParseTree.YieldStatement tree)
        {
            return new YieldStatement(outer, tree);
        }

        private static IStatement Visit(IScoped outer, ParseTree.BreakStatement tree)
        {
            return new Break(outer, tree);
        }

        private static IStatement Visit(IScoped outer, ParseTree.GuardStatement tree)
        {
            return new Guard(outer, tree);
        }

        private static IStatement Visit(IScoped outer, ParseTree.DeclarationStatement tree)
        {
            return IDeclaration.Build(outer, tree.Declaration);
        }

        private static IStatement Visit(IScoped outer, ParseTree.ExpressionStatement tree)
        {
            return IExpression.Build(outer, tree.Expression);
        }

        private static IStatement Visit(IScoped outer, ParseTree.IfStatement tree)
        {
            return new If(outer, tree);
        }

        private static IStatement Visit(IScoped outer, ParseTree.WhileStatement tree)
        {
            return new While(outer, tree);
        }

        private static IStatement Visit(IScoped outer, ParseTree.RepeatStatement tree)
        {
            return new Repeat(outer, tree);
        }

        private static IStatement Visit(IScoped outer, ParseTree.SwitchStatement tree)
        {
            return new Switch(outer, tree);
        }

        private static IStatement Visit(IScoped outer, ParseTree.ForInStatement tree)
        {
            return new ForIn(outer, tree);
        }

        private static IStatement Visit(IScoped outer, ParseTree.LabeledStatement tree)
        {
            return new Labeled(outer, tree);
        }

        private static IStatement Visit(IScoped outer, ParseTree.IStatement tree)
        {
            return new Dummy(outer, tree);
        }
    }
}
