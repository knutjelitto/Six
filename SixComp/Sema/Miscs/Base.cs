using SixComp.Support;

namespace SixComp.Sema
{
    public abstract class Base : IScoped, IReportable
    {
        public Base(IScoped outer)
        {
            Outer = outer;
        }

        public IScoped Outer { get; }
        public IScope Scope => Outer.Scope;

        public abstract void Report(IWriter writer);
    }
}
