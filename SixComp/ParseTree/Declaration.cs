using SixComp.Support;

namespace SixComp.ParseTree
{
    public abstract class Declaration : IWriteable
    {
        public abstract void Write(IWriter writer);
    }
}
