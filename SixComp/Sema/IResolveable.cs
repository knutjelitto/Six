using SixComp.Support;

namespace SixComp.Sema
{
    public interface IResolveable
    {
        void Resolve(IWriter writer);
    }
}
