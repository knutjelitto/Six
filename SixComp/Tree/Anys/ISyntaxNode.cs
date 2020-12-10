using SixComp.Support;

namespace SixComp
{
    public partial class ParseTree
    {
        public interface ISyntaxNode : IWritable
        {
            NodeData? Data { get; }
        }
    }
}