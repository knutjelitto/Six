namespace SixComp.Sema
{
    public abstract class Base<TTree> : Base, IWithTree<TTree>
    {
        protected Base(IScoped outer, TTree tree)
            : base(outer)
        {
            Tree = tree;
        }

        public TTree Tree { get; }
    }
}
