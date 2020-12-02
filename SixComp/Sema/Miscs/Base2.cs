namespace SixComp.Sema
{
    public abstract class Base<TTree> : Base
    {
        protected Base(IScoped outer, TTree tree)
            : base(outer)
        {
            Tree = tree;
        }

        public TTree Tree { get; }
    }
}
