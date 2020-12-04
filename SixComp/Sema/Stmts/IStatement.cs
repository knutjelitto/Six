namespace SixComp.Sema
{
    public interface IStatement : IScoped, IReportable
    {
        public static IStatement? MaybeBuild(IScoped scope, Tree.AnyStatement? tree)
        {
            if (tree == null)
            {
                return null;
            }
            return Visit(scope, (dynamic)tree);
        }

        public static IStatement Build(IScoped outer, Tree.AnyStatement tree)
        {
            return Visit(outer, (dynamic)tree);
        }

        private static IStatement Visit(IScoped outer, Tree.CodeBlock tree)
        {
            return new CodeBlock(outer, tree);
        }

        private static IStatement Visit(IScoped outer, Tree.ReturnStatement tree)
        {
            return new Return(outer, tree);
        }

        private static IStatement Visit(IScoped outer, Tree.DeferStatement tree)
        {
            return new Defer(outer, tree);
        }

        private static IStatement Visit(IScoped outer, Tree.YieldStatement tree)
        {
            return new YieldStatement(outer, tree);
        }

        private static IStatement Visit(IScoped outer, Tree.BreakStatement tree)
        {
            return new Break(outer, tree);
        }

        private static IStatement Visit(IScoped outer, Tree.GuardStatement tree)
        {
            return new Guard(outer, tree);
        }

        private static IStatement Visit(IScoped outer, Tree.DeclarationStatement tree)
        {
            return IDeclaration.Build(outer, tree.Declaration);
        }

        private static IStatement Visit(IScoped outer, Tree.ExpressionStatement tree)
        {
            return IExpression.Build(outer, tree.Expression);
        }

        private static IStatement Visit(IScoped outer, Tree.IfStatement tree)
        {
            return new If(outer, tree);
        }

        private static IStatement Visit(IScoped outer, Tree.WhileStatement tree)
        {
            return new While(outer, tree);
        }

        private static IStatement Visit(IScoped outer, Tree.RepeatStatement tree)
        {
            return new Repeat(outer, tree);
        }

        private static IStatement Visit(IScoped outer, Tree.SwitchStatement tree)
        {
            return new Switch(outer, tree);
        }

        private static IStatement Visit(IScoped outer, Tree.ForInStatement tree)
        {
            return new ForIn(outer, tree);
        }

        private static IStatement Visit(IScoped outer, Tree.LabeledStatement tree)
        {
            return new Labeled(outer, tree);
        }

        private static IStatement Visit(IScoped outer, Tree.AnyStatement tree)
        {
            return new Dummy(outer, tree);
        }
    }
}
