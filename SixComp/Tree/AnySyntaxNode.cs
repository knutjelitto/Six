using SixComp.Support;

namespace SixComp.Tree
{
    public interface AnySyntaxNode : IWritable
    {
        NodeData? Data { get; }
    }
}
