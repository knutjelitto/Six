using SixComp.Support;

namespace SixComp.Sema
{
    public class VoidType : IType
    {
        public VoidType()
        {
        }

        public void Report(IWriter writer)
        {
            writer.WriteLine("()");
        }
    }
}
