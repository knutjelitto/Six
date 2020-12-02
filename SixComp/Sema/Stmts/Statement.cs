using SixComp.Support;

namespace SixComp.Sema
{
    public abstract class Statement<TTree> : IStatement
        where TTree: Tree.AnyStatement
    {
        public Statement(IScoped outer, TTree tree)
        {
            Tree = tree;
            Outer = outer;
        }

        public IScoped Outer { get; }
        public IScope Scope => Outer.Scope;
        protected TTree Tree { get; }

        public abstract void Report(IWriter writer);
    }
}
