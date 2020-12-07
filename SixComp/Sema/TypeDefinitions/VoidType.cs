using SixComp.Support;

namespace SixComp.Sema
{
    public class VoidType : ITypeDefinition, IResolveable
    {
        public VoidType()
        {
        }

        public void Report(IWriter writer)
        {
            writer.WriteLine("()");
        }

        public void Resolve(IWriter writer)
        {
            //TODO: resolve to some builtin
        }
    }
}
