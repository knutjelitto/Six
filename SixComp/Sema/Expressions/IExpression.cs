using System.Diagnostics;

namespace SixComp.Sema
{
    public interface IExpression: IScoped, IReportable, IStatement
    {
        public static IExpression? MaybeBuild(IScoped outer, ParseTree.IExpression? tree)
        {
            if (tree == null)
            {
                return null;
            }
            return Visit(outer, (dynamic)tree);
        }

        public static IExpression Build(IScoped outer, ParseTree.IExpression tree)
        {
            return Visit(outer, (dynamic)tree);
        }

        private static IExpression Visit(IScoped outer, ParseTree.Initializer tree)
        {
            return Build(outer, tree.Expression);
        }

        private static IExpression Visit(IScoped outer, ParseTree.TryExpression tree)
        {
            return new TryExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, ParseTree.InfixList tree)
        {
            Debug.Assert(tree.Binaries.Count > 0);
            var list = new InfixListExpression(outer, tree);
            outer.Scope.Module.Global.InfixesTodo.Add(list);
            return list;
        }

        private static IExpression Visit(IScoped outer, ParseTree.FunctionCallExpression tree)
        {
            if (tree.IsDrained)
            {
                return Build(outer, tree.Left);
            }
            return new FunctionCallExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, ParseTree.OperatorExpression tree)
        {
            return new OperatorName(outer, tree);
        }

        private static IExpression Visit(IScoped outer, ParseTree.InitializerExpression tree)
        {
            return new InitializerExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, ParseTree.NestedExpression tree)
        {
            return Build(outer, tree.Expression);
        }

        private static IExpression Visit(IScoped outer, ParseTree.InOutExpression tree)
        {
            return new InOutExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, ParseTree.ClosureExpression tree)
        {
            return new ClosureExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, ParseTree.PostfixOpExpression tree)
        {
            return new PostfixExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, ParseTree.NameExpression tree)
        {
            return new NameExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, ParseTree.ILiteralExpression tree)
        {
            return new LiteralExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, ParseTree.PrefixExpression tree)
        {
            return new PrefixExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, ParseTree.ArrayLiteral tree)
        {
            return new ArrayLiteralExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, ParseTree.DictionaryLiteral tree)
        {
            return new DictionaryLiteralExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, ParseTree.PostfixSelfExpression tree)
        {
            return new PostfixSelf(outer, tree);
        }

        private static IExpression Visit(IScoped outer, ParseTree.TupleExpression tree)
        {
            return new TupleExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, ParseTree.SubscriptExpression tree)
        {
            return new SubscriptExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, ParseTree.TypeExpression tree)
        {
            return new TypeExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, ParseTree.ImplicitMemberExpression tree)
        {
            return new ImplicitSelectorExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, ParseTree.ExplicitMemberExpression.NamedMemberSelector tree)
        {
            return new ExplicitSelectorExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, ParseTree.ExplicitMemberExpression.TupleMemberSelector tree)
        {
            return new TupleSelectorExpression(outer, tree);
        }

        private static IExpression Visit(IScoped outer, ParseTree.IExpression tree)
        {
            return new Dummy(outer, tree);
        }
    }
}
