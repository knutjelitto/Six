using SixComp.Sema;

namespace SixComp.Entities
{
    public interface IWithGenerics : IWithRestrictions
    {
        GenericParameters Generics { get; }
    }
}
