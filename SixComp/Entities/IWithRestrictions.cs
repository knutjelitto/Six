using SixComp.Sema;

namespace SixComp.Entities
{
    public interface IWithRestrictions : IScoped
    {
        GenericRestrictions Where { get; }
    }
}
