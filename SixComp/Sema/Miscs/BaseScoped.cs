namespace SixComp.Sema
{
    public abstract class BaseScoped<TTree> : Base, IWithTree<TTree>
    {
        protected BaseScoped(IScoped outer, TTree tree)
            : base(outer, new IScope(outer))
        {
            Tree = tree;
        }

        public TTree Tree { get; }
    }
}
