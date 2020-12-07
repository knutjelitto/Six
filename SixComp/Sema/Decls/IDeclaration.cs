namespace SixComp.Sema
{
    public interface IDeclaration : IScoped, IReportable, IResolveable, IStatement
    {
        public static IDeclaration Build(IScoped outer, Tree.AnyDeclaration tree)
        {
            return Visit(outer, (dynamic)tree);
        }

        private static IDeclaration Visit(IScoped outer, Tree.ImportDeclaration tree)
        {
            return new ImportDeclaration(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, Tree.ClassDeclaration tree)
        {
            return new ClassDeclaration(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, Tree.StructDeclaration tree)
        {
            return new StructDeclaration(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, Tree.ProtocolDeclaration tree)
        {
            return new ProtocolDeclaration(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, Tree.EnumDeclaration tree)
        {
            return new EnumDeclaration(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, Tree.ExtensionDeclaration tree)
        {
            return new ExtensionDeclaration(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, Tree.FunctionDeclaration tree)
        {
            return new FunctionDeclaration(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, Tree.InitializerDeclaration tree)
        {
            return new InitDeclaration(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, Tree.DeinitializerDeclaration tree)
        {
            return new DeinitDeclaration(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, Tree.OperatorDeclaration tree)
        {
            var op = new OperatorDeclaration(outer, tree);
            outer.Scope.Module.Global.OperatorsTodo.Add(op);
            return op;
        }

        private static IDeclaration Visit(IScoped outer, Tree.PrecGroupDeclaration tree)
        {
            var group = new PrecedenceGroupDeclaration(outer, tree);
            outer.Scope.Module.Global.PrecedencesTodo.Add(group);
            return group;
        }

        private static IDeclaration Visit(IScoped outer, Tree.LetDeclaration tree)
        {
            return new PatternLet(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, Tree.PatternVarDeclaration tree)
        {
            return new PatternVar(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, Tree.TypealiasDeclaration tree)
        {
            return new TypealiasDeclaration(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, Tree.AssociatedTypeDeclaration tree)
        {
            return new AssociatedTypeDeclaration(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, Tree.SubscriptDeclaration tree)
        {
            return new SubscriptDeclaration(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, Tree.VarDeclaration tree)
        {
            return new BlockVarDeclaration(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, Tree.AnyDeclaration tree)
        {
            return new Dummy(outer, tree);
        }
    }
}
