namespace SixComp.Sema
{
    public abstract class BaseScoped<TTree> : Base, IWithTree<TTree>
    {
        protected BaseScoped(IScoped outer, TTree tree)
            : base(outer, new Scope(outer))
        {
            Tree = tree;
        }

        public TTree Tree { get; }
    }
}
