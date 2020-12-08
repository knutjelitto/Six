using System.Diagnostics;

namespace SixComp.Sema
{
    public interface IExpression: IScoped, IReportable, IStatement
    {
        public static IExpression? MaybeBuild(IScoped outer, Tree.AnyExpression? tree)
        {
            if (tree == null)
            {
                return null;
            }
            return Visit(outer, (dynamic)tree);
        }

        public static IExpression Build(IScoped outer, Tree.AnyExpression tree)
        {
            return Visit(outer, (dynamic)tree);
        }

        private static IExpression Visit(IScoped outer, Tree.Initializer tree)
        {
            return Build(outer, tree.Expression);
        }

        private static IExpression Visit(IScoped outer, Tree.TryExpression tree)
        {
            return new TryExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.InfixList tree)
        {
            Debug.Assert(tree.Binaries.Count > 0);
            var list = new InfixListExpression(outer, tree);
            outer.Scope.Module.Global.InfixesTodo.Add(list);
            return list;
        }

        private static IExpression Visit(IScoped outer, Tree.FunctionCallExpression tree)
        {
            if (tree.IsDrained)
            {
                return Build(outer, tree.Left);
            }
            return new FunctionCallExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.OperatorExpression tree)
        {
            return new OperatorName(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.InitializerExpression tree)
        {
            return new InitializerExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.NestedExpression tree)
        {
            return Build(outer, tree.Expression);
        }

        private static IExpression Visit(IScoped outer, Tree.InOutExpression tree)
        {
            return new InOutExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.ClosureExpression tree)
        {
            return new ClosureExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.PostfixOpExpression tree)
        {
            return new PostfixExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.NameExpression tree)
        {
            return new NameExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.AnyLiteralExpression tree)
        {
            return new LiteralExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.PrefixExpression tree)
        {
            return new PrefixExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.ArrayLiteral tree)
        {
            return new ArrayLiteralExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.DictionaryLiteral tree)
        {
            return new DictionaryLiteralExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.PostfixSelfExpression tree)
        {
            return new PostfixSelf(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.TupleExpression tree)
        {
            return new TupleExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.SubscriptExpression tree)
        {
            return new SubscriptExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.TypeExpression tree)
        {
            return new TypeExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.ImplicitMemberExpression tree)
        {
            return new ImplicitSelectorExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.ExplicitMemberExpression.NamedMemberSelector tree)
        {
            return new ExplicitSelectorExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.ExplicitMemberExpression.TupleMemberSelector tree)
        {
            return new TupleSelectorExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.AnyExpression tree)
        {
            return new Dummy(outer, tree);
        }
    }
}
