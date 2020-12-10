namespace SixComp.Sema
{
    public interface IDeclaration : IScoped, IReportable, IStatement
    {
        public static IDeclaration Build(IScoped outer, ParseTree.IDeclaration tree)
        {
            return Visit(outer, (dynamic)tree);
        }

        private static IDeclaration Visit(IScoped outer, ParseTree.ImportDeclaration tree)
        {
            return new ImportDeclaration(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, ParseTree.ClassDeclaration tree)
        {
            return new ClassDeclaration(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, ParseTree.StructDeclaration tree)
        {
            return new StructDeclaration(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, ParseTree.ProtocolDeclaration tree)
        {
            return new ProtocolDeclaration(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, ParseTree.EnumDeclaration tree)
        {
            return new EnumDeclaration(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, ParseTree.ExtensionDeclaration tree)
        {
            return new ExtensionDeclaration(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, ParseTree.FunctionDeclaration tree)
        {
            return new FunctionDeclaration(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, ParseTree.InitializerDeclaration tree)
        {
            return new InitDeclaration(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, ParseTree.DeinitializerDeclaration tree)
        {
            return new DeinitDeclaration(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, ParseTree.OperatorDeclaration tree)
        {
            var op = new OperatorDeclaration(outer, tree);
            outer.Scope.Module.Global.OperatorsTodo.Add(op);
            return op;
        }

        private static IDeclaration Visit(IScoped outer, ParseTree.PrecGroupDeclaration tree)
        {
            var group = new PrecedenceGroupDeclaration(outer, tree);
            outer.Scope.Module.Global.PrecedencesTodo.Add(group);
            return group;
        }

        private static IDeclaration Visit(IScoped outer, ParseTree.LetDeclaration tree)
        {
            return new PatternLet(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, ParseTree.PatternVarDeclaration tree)
        {
            return new PatternVar(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, ParseTree.TypealiasDeclaration tree)
        {
            return new TypealiasDeclaration(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, ParseTree.AssociatedTypeDeclaration tree)
        {
            return new AssociatedTypeDeclaration(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, ParseTree.SubscriptDeclaration tree)
        {
            return new SubscriptDeclaration(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, ParseTree.VarDeclaration tree)
        {
            return new BlockVarDeclaration(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, ParseTree.IDeclaration tree)
        {
            return new Dummy(outer, tree);
        }
    }
}
