using Six.Support;

namespace SixComp.Sema
{
    public interface IReportable
    {
        void Report(IWriter writer);
    }
}
