using SixComp.Sema.Decls;

namespace SixComp.Sema
{
    public interface IDeclaration : IScoped, IReportable, IStatement
    {
        public static IDeclaration Build(IScoped outer, Tree.AnyDeclaration tree)
        {
            return Visit(outer, (dynamic)tree);
        }

        private static IDeclaration Visit(IScoped outer, Tree.ClassDeclaration tree)
        {
            return new Class(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, Tree.StructDeclaration tree)
        {
            return new Struct(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, Tree.ProtocolDeclaration tree)
        {
            return new Protocol(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, Tree.ExtensionDeclaration tree)
        {
            return new Extension(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, Tree.FunctionDeclaration tree)
        {
            return new Func(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, Tree.OperatorDeclaration tree)
        {
            var op = new OperatorDecl(outer, tree);
            outer.Scope.Package.OperatorsTodo.Enqueue(op);
            return op;
        }

        private static IDeclaration Visit(IScoped outer, Tree.PrecGroupDeclaration tree)
        {
            var group = new PrecedenceGroup(outer, tree);
            outer.Scope.Package.PrecedencesTodo.Enqueue(group);
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
            return new Typealias(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, Tree.AssociatedTypeDeclaration tree)
        {
            return new AssociatedType(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, Tree.SubscriptDeclaration tree)
        {
            return new SubscriptDecl(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, Tree.VarDeclaration tree)
        {
            return new BlockVar(outer, tree);
        }

        private static IDeclaration Visit(IScoped outer, Tree.AnyDeclaration tree)
        {
            return new Dummy(outer, tree);
        }
    }
}
