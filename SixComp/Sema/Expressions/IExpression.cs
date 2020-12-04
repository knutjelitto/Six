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

        private static IExpression Visit(IScoped outer, Tree.Expression tree)
        {
            if (tree.Binaries.Count > 0)
            {
                var list = new InfixList(outer, tree);
                outer.Scope.Package.Global.InfixesTodo.Add(list);
                return list;
            }
            return Build(outer, tree.Left);
        }

        private static IExpression Visit(IScoped outer, Tree.FunctionCallExpression tree)
        {
            if (tree.IsDrained)
            {
                return Build(outer, tree.Left);
            }
            return new FuncCall(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.OperatorExpression tree)
        {
            return new OperatorName(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.InitializerExpression tree)
        {
            return new Initializer(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.NestedExpression tree)
        {
            return Build(outer, tree.Expression);
        }

        private static IExpression Visit(IScoped outer, Tree.InOutExpression tree)
        {
            return new InOut(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.ClosureExpression tree)
        {
            return new Closure(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.PostfixOpExpression tree)
        {
            return new Postfix(outer, tree);
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
            return new ArrayLiteral(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.DictionaryLiteral tree)
        {
            return new DictionaryLiteral(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.PostfixSelfExpression tree)
        {
            return new PostfixSelf(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.TupleExpression tree)
        {
            return new Tuple(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.SubscriptExpression tree)
        {
            return new Subscript(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.TypeExpression tree)
        {
            return new TypeExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.ImplicitMemberExpression tree)
        {
            return new ImplicitSelector(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.ExplicitMemberExpression.NamedMemberSelector tree)
        {
            return new ExplicitSelector(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.ExplicitMemberExpression.TupleMemberSelector tree)
        {
            return new TupleSelector(outer, tree);
        }

        private static IExpression Visit(IScoped outer, Tree.AnyExpression tree)
        {
            return new Dummy(outer, tree);
        }
    }
}
