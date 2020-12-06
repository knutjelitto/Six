using SixComp.Support;

namespace SixComp.Sema
{
    public class VoidType : ITypeDefinition
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
