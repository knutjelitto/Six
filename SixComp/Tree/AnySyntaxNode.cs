using SixComp.Support;

namespace SixComp
{
    public partial class Tree
    {
        public interface AnySyntaxNode : IWritable
        {
            NodeData? Data { get; }
        }
    }
}