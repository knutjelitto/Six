namespace SixComp.Sema
{
    public interface IScope
    {
        IScoped Parent { get; }
        Package Package { get; }
        Global Global { get; }

        void AddUnique(INamed named);
    }
}
