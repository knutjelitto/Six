namespace SixComp.Sema
{
    public interface IScoped 
    {
        IScope Scope { get; }
        IScoped Outer { get; }
    }
}
