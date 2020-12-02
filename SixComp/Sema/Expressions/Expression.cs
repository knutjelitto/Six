namespace SixComp.Sema
{
    public abstract class Expression<TTree> : Base<TTree>, IExpression
        where TTree: Tree.AnyExpression
    {
        public Expression(IScoped outer, TTree tree)
            : base(outer, tree)
        {
        }
    }
}
