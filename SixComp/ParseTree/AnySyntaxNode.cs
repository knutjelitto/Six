using SixComp.Support;

namespace SixComp.ParseTree
{
    public interface AnySyntaxNode : IWritable
    {
        NodeData? Data { get; }
    }
}
