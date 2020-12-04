namespace SixComp.Sema
{
    public interface IWhere : IScoped
    {
        GenericRestrictions Where { get; }
    }
}
