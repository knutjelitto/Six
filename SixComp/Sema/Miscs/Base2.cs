namespace SixComp.Sema
{
    public abstract class Base<TTree> : Base, IWithTree<TTree>
    {
        public static readonly object NoTree = new object();

        protected Base(IScoped outer, TTree tree)
            : base(outer)
        {
            Tree = tree;
        }

        public TTree Tree { get; }
    }
}
