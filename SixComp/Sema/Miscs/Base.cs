using SixComp.Support;

namespace SixComp.Sema
{
    public abstract class Base : IScoped, IReportable
    {
        public Base(IScoped outer, IScope? scope = null)
        {
            Outer = outer;
            Scope = scope ?? outer.Scope;
        }

        public IScoped Outer { get; }
        public IScope Scope { get; }

        public abstract void Report(IWriter writer);
    }
}
