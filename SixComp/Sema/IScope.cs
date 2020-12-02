namespace SixComp.Sema
{
    public interface IScope
    {
        IScoped Parent { get; }
        Package Package { get; }

        void AddUnique(INamed named);
    }
}
